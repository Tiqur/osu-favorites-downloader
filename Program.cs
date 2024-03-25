using static EnviornmentVariables;

var ci = new ConsoleInterface();
ci.PrintHeader("output", "14852499");
ci.UpdateStatusLine($"Downloaing mapset: {28310983}");
ci.UpdateSpinner();
Thread.Sleep(500);
ci.UpdateSpinner();
ci.UpdateProgressBar(16, 18, 32);
Thread.Sleep(500);
ci.UpdateSpinner();
ci.UpdateProgressBar(18, 18, 32);





//var osu = new OsuAPIWrapper();
//
//// Authenticate instance
//await osu.Authenticate(CLIENT_SECRET, CLIENT_ID);
//
//// Get User
//var USER_ID = "14852499";//GetUserID(); 
//
//// Fetch all favorites for user
//HashSet<string> map_ids = await osu.FetchFavorites(USER_ID);
//
//foreach (string map_id in map_ids)
//{
//  Console.WriteLine($"Downloading: {map_id}");
//  var bytes = await osu.GetBeatmapSetBytes(map_id);
//  WriteBytesToOSZ($"output/{map_id}.osz", bytes);
//}
//
//
//
//void WriteBytesToOSZ(string path, byte[] bytes)
//{
//  try
//  {
//    File.WriteAllBytes(path, bytes);
//  }
//  catch (Exception e)
//  {
//    Console.WriteLine(e);
//  }
//}
//
//string GetUserID()
//{
//  bool valid_client_id = false;
//  string user_id = "";
//
//  while (!valid_client_id)
//  {
//    string? resp = ci.QueryUser("Please enter a user id:");
//    if (resp != null && !osu.ValidateUser(resp)) 
//    {
//      valid_client_id = true;
//    }
//  }
//  return user_id;
//}
