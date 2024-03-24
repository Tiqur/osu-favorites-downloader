using static EnviornmentVariables;

var ci = new ConsoleInterface();
var osu = new OsuAPIWrapper();

// Authenticate instance
await osu.Authenticate(CLIENT_SECRET, CLIENT_ID);
await osu.GetFavorites("14852499");

ci.PrintHeader();

var USER_ID = GetUserID(); 

string GetUserID()
{
  bool valid_client_id = false;
  string user_id = "";

  while (!valid_client_id)
  {
    string? resp = ci.QueryUser("Please enter a user id:");
    if (resp != null && !osu.ValidateUser(resp)) 
    {
      valid_client_id = true;
    }
  }
  return user_id;
}
