using System.Diagnostics;
using System.Runtime.InteropServices;
using ConsoleRenderer;
using W = ConsoleHelperLibrary.Classes.WindowUtility;

public class Program
{
	static int Main(string[] args)
	{
        
		W.DisableResizing();

		Console.Title = "OSRL - (ESC to Quit)";
		W.SetConsoleWindowPosition(W.AnchorWindow.Fill);
        Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
		Console.CursorVisible = false;		

		DisplayManager.Initialize();
		UIManager.Initialize();
		
		UI_SolidFill backgroundFill = new UI_SolidFill ('_');
		backgroundFill.SetAnchorPoint(AnchorPointHorizonal.stretch, AnchorPointVertical.stretch);
        backgroundFill.SetColors(ConsoleColor.DarkGray, ConsoleColor.DarkBlue);

		UI_Panel panel1 = new UI_Panel ("panel1", 0, 4, 30, 30, BorderType.doubleLine, "Panel 1");
        panel1.SetColors(ConsoleColor.White, ConsoleColor.Black);
		panel1.SetAnchorPoint(AnchorPointHorizonal.left, AnchorPointVertical.middle);
		panel1.SetScreenPosition(0,0);
		//panel1.SetOffset(10,-5,10,5);
		panel1.SetSize(30, 30);
		
		// UI_TextArea areaA = new UI_TextArea ("textAreaA", 0, 2, 0, 4, 
		// 	ConsoleColor.White, 
		// 	ConsoleColor.Blue, 
		// 	ConsoleColor.Gray, 
		// 	ConsoleColor.DarkBlue, 
		// 	"This is a Text Box, couldn't you tell?");
		// areaA.SetAnchorPoint(AnchorPointHorizonal.stretch, AnchorPointVertical.stretch);
		// areaA.SetOffset(1,1,1,1);
		// areaA.AlignmentVertical = TextAlignmentVertical.middle;

		// panel1.AddChild(areaA);

		// UI_TextArea areaA = new UI_TextArea ("textAreaA", 0, 2, 0, 1, 
		// 	ConsoleColor.White, 
		// 	ConsoleColor.Blue, 
		// 	ConsoleColor.Gray, 
		// 	ConsoleColor.DarkBlue, 
		// 	"Text Selection A");
		// UI_TextArea areaB = new UI_TextArea ("textAreaB", 0, 2, 0, 1, 
		// 	ConsoleColor.White, 
		// 	ConsoleColor.Blue, 
		// 	ConsoleColor.Gray, 
		// 	ConsoleColor.DarkBlue, 
		// 	"Text Selection B");
		// UI_TextArea areaC = new UI_TextArea ("textAreaC", 0, 2, 0, 1, 
		// 	ConsoleColor.White, 
		// 	ConsoleColor.Blue, 
		// 	ConsoleColor.Gray, 
		// 	ConsoleColor.DarkBlue, 
		// 	"Text Selection C");

		// areaA.CanFocus = true;
		// //areaB.CanFocus = true;
		// areaC.CanFocus = true;

		// UI_VerticalGroup verticalGroup = new UI_VerticalGroup("vertG", new Point (), 0, 0);
		// verticalGroup.SetAnchorPoint(AnchorPointHorizonal.stretch, AnchorPointVertical.stretch);
		// verticalGroup.SetOffset(1,1,1,1);
		// verticalGroup.ListAnchorPoint = AnchorPointVertical.bottom;
		// panel1.AddChild(verticalGroup);
		// verticalGroup.AddChild(areaC);
		// verticalGroup.AddChild(areaB);
		// verticalGroup.AddChild(areaA);

		// UIHelper.CreateVerticalFocusMappings(new List<UIObject> {areaA, areaB, areaC}, true);

		// UI_Panel panel2 = new UI_Panel ("panel2", 30, 10, 70, 20,
		// 	ConsoleColor.White, 
		// 	ConsoleColor.Blue, 
		// 	ConsoleColor.Gray, 
		// 	ConsoleColor.DarkBlue, 
		// 	BorderType.singleLine, 
		// 	"Panel 2 is alright I guess...");

		// UI_TextArea areaA = new UI_TextArea ("textAreaA", 0, 2, 0, 1, 
		// 	"Text Selection A");
		// areaA.UseOffsetLeftRight = true;
		// areaA.OffsetLeft = 1;
		// areaA.OffsetRight = 1;
		// areaA.CanFocus = true;
		// UI_TextArea areaB = new UI_TextArea ("textAreaB", 0, 4, 0, 1, 
		// 	"This is Text Selection B");
		// areaB.UseOffsetLeftRight = true;
		// areaB.OffsetLeft = 1;
		// areaB.OffsetRight = 1;
		// areaB.CanFocus = true;
		// UI_TextArea areaC = new UI_TextArea ("textAreaB", 0, 4, 0, 1, 
		// 	"On the right");
		// areaC.UseOffsetLeftRight = true;
		// areaC.OffsetLeft = 20;
		// areaC.OffsetRight = 1;
		// areaC.CanFocus = true;

		// areaA.SetFocusRelation(areaB, NavigationDirection.down);
		// areaA.SetFocusRelation(areaC, NavigationDirection.right);
		// areaC.SetFocusRelation(areaA, NavigationDirection.left);
		// areaB.SetFocusRelation(areaA, NavigationDirection.up);

		// panel2.AddChild(areaB);
		// panel2.AddChild(areaC);
		// panel2.AddChild(areaA);

		// UIManager.RegisterUIObject(panel2, true);
		UIManager.RegisterUIObject(backgroundFill, false);
		UIManager.RegisterUIObject(panel1, true);

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
				UIManager.Navigate(NavigationDirection.right);
			}
			else if (keyInfo.Key == ConsoleKey.LeftArrow)
			{
				UIManager.Navigate(NavigationDirection.left);			
			}
			else if (keyInfo.Key == ConsoleKey.UpArrow)
			{
				UIManager.Navigate(NavigationDirection.up);
			}
			else if (keyInfo.Key == ConsoleKey.DownArrow)
			{
				UIManager.Navigate(NavigationDirection.down);
			}

			if (keyInfo.Key == ConsoleKey.F1)
			{
				UIManager.UpdateResolution();
			}

			UIManager.Draw();
			DisplayManager.Render();
		}
		while(keyInfo.Key != ConsoleKey.Escape);
		

		
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

