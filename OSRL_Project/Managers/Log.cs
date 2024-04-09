using System.Diagnostics;
using System.Reflection.Metadata;

public static class Log
{
	// TODO - i/o to file

    public const bool CANLOG_WRITELINE = true;
    public const bool CANLOG_WARNING = true;
    public const bool CANLOG_ERROR = true;

    public static void Initialize()
    {
        // TODO
        // System.IO.Directory.CreateDirectory("Logs");
        // string fileName = DateTime.Now.ToString("yyyy-MM-dddd_HH:mm:ss") + ".txt";
        // FileStream stream = File.Create(fileName);
    }
    
    /// <summary>
    /// Logs a warning to the console.
    /// </summary>
	public static void Warning(string text)
	{
        if (!CANLOG_WARNING)
        {
            return;
        }
		Debug.WriteLine("WARNING: " + text);
	}

    /// <summary>
    /// Logs an error to the console.
    /// </summary>
	public static void Error(string text)
	{
        if (!CANLOG_ERROR)
        {
            return;
        }
		Debug.WriteLine("ERROR: " + text);
	}

    /// <summary>
    /// Logs a line to the console.
    /// </summary>
	public static void WriteLine(string text)
	{
        if (!CANLOG_WRITELINE)
        {
            return;
        }
		Debug.WriteLine("Log: " + text);
	}
}