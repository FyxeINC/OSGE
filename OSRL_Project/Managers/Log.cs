using System.Diagnostics;

public static class Log
{
	// TODO - i/o to file

    public static void Initialize()
    {
        
    }
	
	public static void Warning(string text)
	{
		Debug.WriteLine("WARNING: " + text);
	}

	public static void Error(string text)
	{
		Debug.WriteLine("ERROR: " + text);
	}

	public static void WriteLine(string text)
	{
		Debug.WriteLine("Log: " + text);
	}
}