enum Color
{
  BLACK = 30,
  RED = 31,
  GREEN = 32,
  YELLOW = 33,
  BLUE = 34,
  MAGENTA = 35,
  CYAN = 36,
  WHITE = 37,
  DEFAULT = 39,
  RESET = 0
}



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

  private void SetForegroundColor(Color color)
  {
    Console.WriteLine($"\x1b[0;{color}m");
  }

  private void SetBackgroundColor(Color color)
  {
    Console.WriteLine($"\x1b[0;{color+10}m");
  }

  public void PrintHeader(string output_dir, string user_id)
  {
    SetBackgroundColor(Color.DEFAULT);
    SetForegroundColor(Color.DEFAULT);
    Clear();
    MoveCursorTo(0, 0);
    Console.WriteLine($"Output dir: {output_dir}");
    Console.WriteLine("Average kbps:");
    Console.WriteLine($"Downloading favorite beatmaps for user: {user_id}");
    Console.WriteLine("");
    UpdateProgressBar(13, 18, 32);

  }

  public void UpdateProgressBar(int current, int max, int progress_bar_length)
  {

    // Percent done
    double percent = (double)current / max;

    // How much of progress bar to fill
    double progress = percent * progress_bar_length;

    // Build progress bar
    string bar = "";
    for (int i = 0; i < progress_bar_length; i++)
      bar += i <= progress ? '=' : '-';

    Console.WriteLine($"[{bar}] {Math.Round(percent*100, 1)}%");
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
