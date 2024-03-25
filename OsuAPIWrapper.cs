using System.Text.Json;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;

public class OsuAPIWrapper
{
  const string endpoint = "https://osu.ppy.sh/api/v2/";
  private static HttpClient sharedClient = new(){BaseAddress = new Uri(endpoint)};
  private int? token_expiration;
  private string? access_token;
  private string? refresh_token;

  public class OsuAuth {
    public string? token_type {get; set;}
    public int? expires_in {get; set;}
    public string? access_token {get; set;}
    public string? refresh_token {get; set;}
  };

  public async Task<byte[]> GetBeatmapSetBytes(string beatmapset_id)
  {
    var resp = await sharedClient.GetAsync($"https://beatconnect.io/b/{beatmapset_id}");
    resp.EnsureSuccessStatusCode();

    return await resp.Content.ReadAsByteArrayAsync();
  }

  public async Task<HashSet<string>> FetchFavorites(string user_id)
  {
    int offset = 0;
    int previous_count = 0;
    HashSet<string> map_ids = new HashSet<string>();

    while (true)
    {
      var resp = await sharedClient.GetAsync(endpoint+$"users/{user_id}/beatmapsets/favourite?limit=100&offset={offset}");
      resp.EnsureSuccessStatusCode();
      var resp_str = await resp.Content.ReadAsStringAsync();

      // Use regex to extract beatmap ids
      var rg = new Regex(@"""beatmapset_id"":\s*(\d+)");
      var matched_ids = rg.Matches(resp_str);

      // Add to hashset to remove duplicates
      foreach (Match match in matched_ids)
      {
        var map_id = match.Groups[1].Value;
        map_ids.Add(map_id);
      }

      // Break if offset is not 100 or count == prev_count (indicates out of favorited maps)
      // count % 100 will avoid a request, but there is an edgecase if the favorited map count is a multiple of 100
      if (map_ids.Count % 100 != 0 || map_ids.Count == previous_count)
        break;

      offset += 100;
      previous_count = map_ids.Count;

      // Be kind to api
      Thread.Sleep(1000);
    }

    return map_ids;
  }

  private void SetClientDefaultAuthHeader(string token)
  {
    sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
  }

  public async Task Authenticate(string client_secret, string client_id)
  {
    try
    {
      Dictionary<string, string> values = new Dictionary<string, string>
      {
        { "client_id", client_id },
        { "client_secret", client_secret },
        { "grant_type", "client_credentials" },
        { "scope", "public" },
      };

      // Set Data
      var content = new FormUrlEncodedContent(values);

      // Send request
      var resp = await sharedClient.PostAsync("https://osu.ppy.sh/oauth/token", content);
      resp.EnsureSuccessStatusCode();
      
      // Deserialize json to object
      var resp_content = await resp.Content.ReadAsStringAsync();
      OsuAuth? resp_obj = JsonSerializer.Deserialize<OsuAuth>(resp_content);
      if (resp_obj == null)
      {
        throw new JsonException("Something went wrong deserializing osu authentication response");
      }

      token_expiration = resp_obj.expires_in;
      access_token = resp_obj.access_token;
      refresh_token = resp_obj.refresh_token;
      SetClientDefaultAuthHeader(access_token ?? "");
    }
    catch (Exception e)
    {
      Console.WriteLine($"Error: {e.Message}");
    }
  }

  public bool ValidateUser(string user_id)
  {
    return true;
  }
}
