using System.Diagnostics;

public static class Log
{
	// TODO - i/o to file

    public static void Initialize()
    {
        
    }
    
    /// <summary>
    /// Logs a warning to the console.
    /// </summary>
	public static void Warning(string text)
	{
		Debug.WriteLine("WARNING: " + text);
	}

    /// <summary>
    /// Logs an error to the console.
    /// </summary>
	public static void Error(string text)
	{
		Debug.WriteLine("ERROR: " + text);
	}

    /// <summary>
    /// Logs a line to the console.
    /// </summary>
	public static void WriteLine(string text)
	{
		Debug.WriteLine("Log: " + text);
	}
}