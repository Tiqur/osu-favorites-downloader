using static EnviornmentVariables;

var ci = new ConsoleInterface();
var osu = new OsuAPIWrapper(CLIENT_SECRET, CLIENT_ID);
var USER_ID = GetUserID(); 

Console.WriteLine($"{CLIENT_ID}");
Console.WriteLine($"{CLIENT_SECRET}");
Console.WriteLine($"{USER_ID}");
ci.PrintHeader();

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
