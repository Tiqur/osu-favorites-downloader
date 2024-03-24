public class OsuAPIWrapper
{
  const String endpoint = "https://osu.ppy.sh/api/v2/";
  String? access_token;
  private static HttpClient sharedClient = new()
  {
    BaseAddress = new Uri(endpoint),
  };

  public OsuAPIWrapper(String client_secret, String client_id)
  {
    // Obtain access token
    GetAccessToken(client_secret, client_id);
  }


  private async void GetAccessToken(String client_secret, String client_id)
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

      String resp_string = await resp.Content.ReadAsStringAsync();
      Console.WriteLine(resp_string);
    }
    catch (Exception e)
    {
      Console.WriteLine($"Error: {e.Message}");
    }
    //return "access_token_placeholder";
  }

  public bool ValidateUser(String user_id)
  {
    return true;
  }
}
