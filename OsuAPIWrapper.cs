using System.Text.Json;
using System.Net;


public class OsuAPIWrapper
{
  const string endpoint = "https://osu.ppy.sh/api/v2/";
  private static HttpClient sharedClient = new(){BaseAddress = new Uri(endpoint)};
  Task<OsuAuth?> osu_auth;


  public OsuAPIWrapper(string client_secret, string client_id)
  {
    // Obtain access token
    this.osu_auth = GetAccessToken(client_secret, client_id);
  }

  public class OsuAuth {
    public string? token_type {get; set;}
    public int? expires_in {get; set;}
    public string? access_token {get; set;}
    public string? refresh_token {get; set;}
  };


  private async Task<OsuAuth?> GetAccessToken(string client_secret, string client_id)
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

      if (resp.StatusCode != HttpStatusCode.OK)
      {
        throw new HttpRequestException(await resp.Content.ReadAsStringAsync());
      }
      
      // Deserialize json to object
      var resp_content = await resp.Content.ReadAsStringAsync();
      OsuAuth? resp_obj = JsonSerializer.Deserialize<OsuAuth>(resp_content);
      if (resp_obj == null)
      {
        throw new JsonException("Something went wrong deserializing osu authentication response");
      }

      return resp_obj;
    }
    catch (Exception e)
    {
      Console.WriteLine($"Error: {e.Message}");
    }

    return null;
  }

  public bool ValidateUser(string user_id)
  {
    return true;
  }
}
