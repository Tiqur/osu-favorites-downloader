public class OsuFavoritesDownloader {
  public static void Main(String[] args)
  {
    // Create new ConsoleInterface
    ConsoleInterface ci = new ConsoleInterface();

    // Check ENV for CLIENT_SECRET
    String? client_secret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
    if (client_secret == null)
    {
      throw new ArgumentNullException("CLIENT_SECRET environment variable must be set.");
    }

    // Get osu! client ID
    // TODO
    
    ci.PrintHeader();
  }
}
