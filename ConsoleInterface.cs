public class ConsoleInterface
{
  public ConsoleInterface() 
  {
  }

  public void PrintHeader()
  {

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
