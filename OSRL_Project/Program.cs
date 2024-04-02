using ConsoleRenderer;
using W = ConsoleHelperLibrary.Classes.WindowUtility;

public class Program
{
	protected static Display m_Display;

	static int Main(string[] args)
	{
		Console.Title = "Roomy";
		W.SetConsoleWindowPosition(W.AnchorWindow.Fill);
		
		ConsoleRenderer.ConsoleCanvas Canvas = new ConsoleCanvas (false, false);
		Canvas.CreateBorder();
		Canvas.Render();
		//m_Display = new Display ();
		//m_Display.Render();
		Console.ReadKey();
			
		return 0;
	}
}

