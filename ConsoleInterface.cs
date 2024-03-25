public class ConsoleInterface
{
  public ConsoleInterface() 
  {
  }

  private void MoveCursorTo(int column, int line)
  {
    Console.WriteLine($"\x1b[{line};{column}H ");
  }

  private void Clear()
  {
    Console.WriteLine("\x1b[2J");
  }

  public void PrintHeader(string output_dir, string user_id)
  {
    Clear();
    MoveCursorTo(0, 0);
    Console.WriteLine($"Output dir: {output_dir}");
    Console.WriteLine("Average kbps:");
    Console.WriteLine($"Downloading favorite beatmaps for user: {user_id}");
    Console.WriteLine("");
    Console.WriteLine("[]");

  }

  public void UpdateProgressBar()
  {

  }

  public void UpdateStatusLine()
  {

  }

  public void UpdateSpinner()
  {

  }

  public string QueryUser(string query)
  {
    Console.WriteLine(query);
    return Console.ReadLine() ?? throw new Exception("Something went wrong reading line");
  }

}
