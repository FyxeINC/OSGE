using System.Diagnostics;
using ConsoleRenderer;
using W = ConsoleHelperLibrary.Classes.WindowUtility;

public class Program
{
	protected static Display m_Display;

	static int Main(string[] args)
	{
		Console.Title = "OSRL";
		//W.SetConsoleWindowPosition(W.AnchorWindow.Fill);
		
		Transform parent = new Transform (AnchorType.topLeft, new Point (), 100, 100, new TransformOffset ());
		Transform childTopleft = new Transform (AnchorType.topLeft, new Point (5,10), 50, 50, new TransformOffset (), parent);
		Transform childStretch = new Transform (AnchorType.stretch, new Point (), 0, 0, new TransformOffset (10, 5, 15, 2), parent);

		Rect topLeft = childTopleft.GetRect();
		Rect stretch = childStretch.GetRect();

		Debug.Print(topLeft.X + " " + topLeft.Y + " " + topLeft.Width + " " + topLeft.Height);
		Debug.Print(stretch.X + " " + stretch.Y + " " + stretch.Width + " " + stretch.Height);




		
		UIParent uiParent = UIManager.CreateUIObject<UIParent>(true);
		UIE_SolidColor elementSolidColor = uiParent.AddElement<UIE_SolidColor>("SolidColor");
		elementSolidColor.DisplayChar = ' ';
		elementSolidColor.Foreground = ConsoleColor.White;
		elementSolidColor.Background = ConsoleColor.Black;
		
		UIManager.RenderAll();

		//m_Display = new Display ();
		//m_Display.Render();
		Console.ReadKey();
			
		return 0;
	}
}

