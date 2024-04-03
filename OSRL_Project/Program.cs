using System.Diagnostics;
using ConsoleRenderer;
using W = ConsoleHelperLibrary.Classes.WindowUtility;

public class Program
{
	static int Main(string[] args)
	{
		Console.Title = "OSRL - Open Source Rouge Like";
		W.SetConsoleWindowPosition(W.AnchorWindow.Fill);
		Console.CursorVisible = false;		

		DisplayManager.Initialize();
		UIManager.Initialize();
		
		UI_SolidFill solidColor1 = new UI_SolidFill ("UIColor1",0,0,0,0, 
			ConsoleColor.DarkCyan, 
			ConsoleColor.DarkCyan,
			ConsoleColor.DarkCyan, 
			ConsoleColor.DarkCyan,
			'.');
		solidColor1.UseOffsetLeftRight = true;
		solidColor1.UseOffsetTopBottom = true;

		UI_Panel panel1 = new UI_Panel ("panel1", 0, 4, 30, 30, 
			ConsoleColor.White, 
			ConsoleColor.DarkGray, 
			ConsoleColor.Gray, 
			ConsoleColor.Black, 
			BorderType.doubleLine, 
			"Panel 1 is the greatest panel ever.");
		panel1.UseOffsetTopBottom = true;
		panel1.OffsetTop = 30;
		panel1.OffsetBottom = 20;
		panel1.UseOffsetLeftRight = true;
		panel1.OffsetLeft= 10;
		panel1.OffsetRight = 20;

		UI_Panel panel2 = new UI_Panel ("panel2", 30, 30, 70, 20,
			ConsoleColor.White, 
			ConsoleColor.Blue, 
			ConsoleColor.Gray, 
			ConsoleColor.DarkBlue, 
			BorderType.doubleLine, 
			"Panel 2 is alright I guess...");

		UIManager.RegisterUIObject(panel1, true);
		UIManager.RegisterUIObject(panel2, true);
		UIManager.RegisterUIObject(solidColor1, false);

		UIManager.Draw();
		DisplayManager.Render();

		ConsoleKeyInfo keyInfo = new ConsoleKeyInfo ();
		do
		{
			keyInfo = Console.ReadKey(true);

			// if (keyInfo.Key == ConsoleKey.RightArrow)
			// {
			// 	panel1.SetPosition(panel1.GetPosition().X + 10, panel1.GetPosition().Y);
			// }
			// else if (keyInfo.Key == ConsoleKey.LeftArrow)
			// {
			// 	panel1.SetPosition(panel1.GetPosition().X - 10, panel1.GetPosition().Y);
			// }
			// else if (keyInfo.Key == ConsoleKey.UpArrow)
			// {
			// 	panel1.SetPosition(panel1.GetPosition().X, panel1.GetPosition().Y - 10);
			// }
			// else if (keyInfo.Key == ConsoleKey.DownArrow)
			// {
			// 	panel1.SetPosition(panel1.GetPosition().X, panel1.GetPosition().Y + 10);
			// }
			if (keyInfo.Key == ConsoleKey.RightArrow)
			{
				UIManager.SetCurrentFrontmostObject(panel1);
			}
			else if (keyInfo.Key == ConsoleKey.LeftArrow)
			{
				UIManager.SetCurrentFrontmostObject(panel2);				
			}

			UIManager.Draw();
			DisplayManager.Render();
		}
		while(keyInfo.Key != ConsoleKey.Enter);
		

		
		//Debug.WriteLine(MainUIObject.Width + "  |  " + MainUIObject.Height);
		// Transform parent = new Transform (AnchorType.topLeft, new Point (), 100, 100, new TransformOffset ());
		// Transform childTopleft = new Transform (AnchorType.topLeft, new Point (5,10), 50, 50, new TransformOffset (), parent);
		// Transform childStretch = new Transform (AnchorType.stretch, new Point (), 0, 0, new TransformOffset (10, 5, 15, 2), parent);

		// Rect topLeft = childTopleft.GetRect();
		// Rect stretch = childStretch.GetRect();

		// Debug.Print(topLeft.X + " " + topLeft.Y + " " + topLeft.Width + " " + topLeft.Height);
		// Debug.Print(stretch.X + " " + stretch.Y + " " + stretch.Width + " " + stretch.Height);

		
		// UIParent uiParent = UIManager.CreateUIObject<UIParent>(true);
		// UIE_SolidColor elementSolidColor = uiParent.AddUIElement<UIE_SolidColor>("SolidColor");
		// elementSolidColor.DisplayChar = ' ';
		// elementSolidColor.Foreground = ConsoleColor.White;
		// elementSolidColor.Background = ConsoleColor.Black;
		
		// UIManager.RenderAll();

		//m_Display = new Display ();
		//m_Display.Render();
			
		return 0;
	}
}

