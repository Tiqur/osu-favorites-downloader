public static class EnviornmentVariables 
{
  public static String CLIENT_SECRET = GetEnviornmentVariables("CLIENT_SECRET");
  public static String CLIENT_ID = GetEnviornmentVariables("CLIENT_ID");

  private static String GetEnviornmentVariables(String name)
  {
    return Environment.GetEnvironmentVariable(name) ?? throw new ArgumentNullException($"Enviornment variable {name} must be set");
  }
}
