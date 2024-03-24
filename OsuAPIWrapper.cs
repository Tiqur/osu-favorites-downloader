public class OsuAPIWrapper
{
  String endpoint = "https://osu.ppy.sh/api/v2/";
  String client_secret;

  public OsuAPIWrapper(String client_secret)
  {
    ValidateSecret(client_secret);
    this.client_secret = client_secret;

  }

  public bool ValidateUser(String user_id)
  {
    return true;
  }

  public void ValidateSecret(String client_secret)
  {
    // TODO: Implement
    if (false)
    {
      throw new ArgumentException("Invalid CLIENT_SECRET");
    }
  }
}
