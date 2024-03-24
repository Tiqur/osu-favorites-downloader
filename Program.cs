var ci = new ConsoleInterface();
var client_secret = GetClientSecret();
var client_id = GetClientID();
var osu = new OsuAPIWrapper(client_secret, client_id);
var user_id = GetUserID(); 

Console.WriteLine($"{client_id}");
Console.WriteLine($"{client_secret}");
Console.WriteLine($"{user_id}");
ci.PrintHeader();


String GetClientSecret()
{
  String? client_secret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
  if (client_secret == null)
  {
    throw new ArgumentNullException("CLIENT_SECRET environment variable must be set.");
  }

  return client_secret;
}

String GetClientID()
{
  String? client_secret = Environment.GetEnvironmentVariable("CLIENT_ID");
  if (client_secret == null)
  {
    throw new ArgumentNullException("CLIENT_ID environment variable must be set.");
  }

  return client_secret;
}

String GetUserID()
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
