public class OsuFavoritesDownloader
{
  static ConsoleInterface ci;
  static OsuAPIWrapper osu;
  static String client_secret;
  static String client_id;
  static String user_id;

  public static void Main(String[] args)
  {
    // Create new ConsoleInterface
    ci = new ConsoleInterface();

    // Check ENV for CLIENT_SECRET
    client_secret = GetClientSecret();

    // Check ENV for CLIENT_ID
    client_id = GetClientID();

    // Create new OsuAPIWrapper
    osu = new OsuAPIWrapper(client_secret, client_id);

    // Get osu! user id from client
    user_id = GetUserID();

    Console.WriteLine($"{client_secret}");
    Console.WriteLine($"{client_id}");
    Console.WriteLine($"{user_id}");
    
    ci.PrintHeader();
  }

  static private String GetClientSecret()
  {
    String? client_secret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
    if (client_secret == null)
    {
      throw new ArgumentNullException("CLIENT_SECRET environment variable must be set.");
    }

    return client_secret;
  }

  static private String GetClientID()
  {
    String? client_secret = Environment.GetEnvironmentVariable("CLIENT_ID");
    if (client_secret == null)
    {
      throw new ArgumentNullException("CLIENT_ID environment variable must be set.");
    }

    return client_secret;
  }

  static private String GetUserID()
  {
    bool valid_client_id = false;
    String user_id = "";

    while (!valid_client_id)
    {
      String? resp = ci.QueryUser("Please enter a user id:");
      if (resp != null && !osu.ValidateUser(resp)) 
      {
        valid_client_id = true;
      }
    }

    return user_id;
  }
}
