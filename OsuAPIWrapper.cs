using System.Text.Json;
using System.Net;


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

  public void Test()
  {
    Console.WriteLine(token_expiration);
    Console.WriteLine(access_token);
    Console.WriteLine(refresh_token);
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

      token_expiration = resp_obj.expires_in;
      access_token = resp_obj.access_token;
      refresh_token = resp_obj.refresh_token;
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
