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
    Console.Write($"\x1b[0;{(int)color}m");
  }

  private void SetBackgroundColor(Color color)
  {
    Console.Write($"\x1b[0;{(int)color+10}m");
  }

  public void PrintHeader(string output_dir, string user_id)
  {
    Clear();
    MoveCursorTo(0, 0);
    SetBackgroundColor(Color.DEFAULT);
    Console.Write("Output dir: ");

    SetForegroundColor(Color.MAGENTA);
    Console.WriteLine("/"+output_dir);

    SetForegroundColor(Color.DEFAULT);
    Console.Write($"Downloading favorite beatmaps for user: ");

    SetForegroundColor(Color.MAGENTA);
    Console.WriteLine(user_id);

    UpdateProgressBar(13, 18, 32);
  }

  public void UpdateProgressBar(int current, int max, int progress_bar_length)
  {
    MoveCursorTo(0, 4);

    // Percent done
    double percent = (double)current / max;

    // How much of progress bar to fill
    double progress = percent * progress_bar_length;

    // Build progress bar
    string bar = "";
    for (int i = 0; i < progress_bar_length; i++)
      bar += i <= progress ? '=' : '-';

    SetForegroundColor(Color.WHITE);
    Console.Write($"[{bar}]");

    SetForegroundColor(Color.GREEN);
    Console.Write($" {current}/{max}");

    SetForegroundColor(Color.RED);
    Console.Write($" 19 bytes/s");

    SetForegroundColor(Color.BLUE);
    Console.Write($" eta 0:00:05");

    SetForegroundColor(Color.DEFAULT);
    Console.WriteLine();

  }

  public void UpdateStatusLine(string text)
  {
    MoveCursorTo(1, 5);
    Console.WriteLine($"  {text}");
  }

  public void UpdateSpinner()
  {
    MoveCursorTo(0, 5);
    Console.Write('/');
  }

  public string QueryUser(string query)
  {
    Console.WriteLine(query);
    return Console.ReadLine() ?? throw new Exception("Something went wrong reading line");
  }

}
