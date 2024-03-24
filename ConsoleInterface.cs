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

  public String? QueryUser(String query)
  {
    Console.WriteLine(query);
    return Console.ReadLine();
  }

}
