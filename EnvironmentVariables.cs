public static class EnviornmentVariables 
{
  public static string CLIENT_SECRET = GetEnviornmentVariables("CLIENT_SECRET");
  public static string CLIENT_ID = GetEnviornmentVariables("CLIENT_ID");

  private static string GetEnviornmentVariables(string name)
  {
    return Environment.GetEnvironmentVariable(name) ?? throw new ArgumentNullException($"Enviornment variable {name} must be set");
  }
}
