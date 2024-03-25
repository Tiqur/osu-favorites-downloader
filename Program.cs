using static EnviornmentVariables;

var ci = new ConsoleInterface();
var osu = new OsuAPIWrapper();

// Authenticate instance
await osu.Authenticate(CLIENT_SECRET, CLIENT_ID);

// Get User
var USER_ID = "14852499";//GetUserID(); 

// Get Path
var PATH = "output";

// Setup terminal UI
ci.PrintHeader(PATH, USER_ID);
ci.StartSpinner();

// Update status
ci.UpdateStatusLine($"Fetching favorited maps...");

// Fetch all favorites for user
HashSet<string> map_ids = await osu.FetchFavorites(USER_ID);
int downloaded_count = 0;

foreach (string map_id in map_ids)
{
  // Update status
  ci.UpdateStatusLine($"Downloaing mapset: {map_id}");

  // Update progress bar
  ci.UpdateProgressBar(downloaded_count, map_ids.Count, 32);

  // Download
  var bytes = await osu.GetBeatmapSetBytes(map_id);

  // Write to disk
  WriteBytesToOSZ($"output/{map_id}.osz", bytes);

  downloaded_count++;
}


void WriteBytesToOSZ(string path, byte[] bytes)
{
  try
  {
    File.WriteAllBytes(path, bytes);
  }
  catch (Exception e)
  {
    Console.WriteLine(e);
  }
}

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
