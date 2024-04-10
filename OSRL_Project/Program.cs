using System.Diagnostics;
using System.Runtime.InteropServices;
using ConsoleRenderer;
using W = ConsoleHelperLibrary.Classes.WindowUtility;

public class Program
{
    public static Action OnQuit;

	static int Main(string[] args)
	{
        AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnApplicationExit);
        
#region Console Setup
		Console.Title = "OSRL - (ESC to Quit)";
		
        // Maximizes window
        // TODO - Create setting and set to last saved setting
        W.SetConsoleWindowPosition(W.AnchorWindow.Fill);

        // Disables resizing of the window
        // TODO - prevent Windowkey + arrow key resizing
		W.DisableResizing();

        // Disables mouse interactions with the screen
        // This prevents selecting parts of the console which in turn causes our renderer to pause displaying
        W.DisableQuickSelect(); 

        // Supposed to fix scrollbar showing up, potentially useless
        // TODO - find a way to disable warning
        Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);

        // TODO - Create cursor manager
		Console.CursorVisible = false;	
#endregion	

#region Initialize Managers
        Log.Initialize();
        PlayerProfileManager.Initialize();
        SaveManager.Initialize();
        AudioManager.Initialize();
        GameObjectManager.Initialize();
		LocalizationManager.Initialize();
		DisplayManager.Initialize();
		UIManager.Initialize();
		InputManager.Initialize();  // Relies on player profile manager
        TimeManager.Initialize();        
        TagManager.Initialize();

        TagManager.instance.Start();
        PlayerProfileManager.instance.Start();
        SaveManager.instance.Start();
        AudioManager.instance.Start();
        LocalizationManager.instance.Start();
        DisplayManager.instance.Start();
        GameObjectManager.instance.Start();
        UIManager.instance.Start();
        InputManager.instance.Start();
        TimeManager.instance.Start();

        InputManager.instance.OnDataSaved();    // DEBUG
#endregion
		
#region DEBUG TESTING
        UI_SolidFill backgroundFill = new UI_SolidFill (' ');
		backgroundFill.SetAnchorPoint(AnchorPointHorizonal.stretch, AnchorPointVertical.stretch);
        backgroundFill.SetColors(ConsoleColor.DarkGray, ConsoleColor.DarkBlue);

		UI_Panel panel1 = new UI_Panel ("panel1", -3, 3, 60, 30, BorderType.doubleLine, "PANEL 1");
        panel1.SetColors(ConsoleColor.White, ConsoleColor.Black);
		panel1.SetAnchorPoint(AnchorPointHorizonal.right, AnchorPointVertical.top);		
		//panel1.SetOffset(10,-5,10,5);
		panel1.SetSize(60, 30);
        
		UI_Panel panel2 = new UI_Panel ("panel2", 0, 4, 30, 30, BorderType.doubleLine, "PANEL 2");
        panel2.SetColors(ConsoleColor.White, ConsoleColor.Black);
		panel2.SetAnchorPoint(AnchorPointHorizonal.left, AnchorPointVertical.middle);
		panel2.SetLocalPosition(0,0);
		//panel1.SetOffset(10,-5,10,5);
		panel2.SetSize(30, 30);

        UI_TextArea textA = new UI_TextArea ("TextA", 1, 1, 5, 1, "AAAAA");
        textA.SetAnchorPoint(AnchorPointHorizonal.left, AnchorPointVertical.top);
        textA.SetColors(ConsoleColor.White, ConsoleColor.Blue);
        textA.SetCanFocus(true);

        UI_TextArea textB= new UI_TextArea ("TextB", 1, 3, 5, 1, "BBBBB");
        textB.SetAnchorPoint(AnchorPointHorizonal.left, AnchorPointVertical.top);
        textB.SetColors(ConsoleColor.White, ConsoleColor.Red);
        textB.SetCanFocus(true);
        

        UI_TextArea textC= new UI_TextArea ("TextC", 7, 1, 5, 1, "CCCCC");
        textC.SetAnchorPoint(AnchorPointHorizonal.left, AnchorPointVertical.top);
        textC.SetColors(ConsoleColor.Black, ConsoleColor.Green);
        textC.SetCanFocus(true);

        panel1.AddChild(textC, true);
        panel1.AddChild(textA, true);
        panel1.AddChild(textB, true);

        Log.WriteLine(textA.GetScreenSpaceRect().ToString());
        Log.WriteLine(textB.GetScreenSpaceRect().ToString());
        Log.WriteLine(textC.GetScreenSpaceRect().ToString());
		
        
		UIManager.instance.AddUIObject(panel1, Tags.UILayer_Game);

		UIManager.instance.AddUIObject(backgroundFill, Tags.UILayer_Game);

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

        //UIManager.Draw();
		//DisplayManager.Render();

		// ConsoleKeyInfo keyInfo = new ConsoleKeyInfo ();
		// do
		// {
        //     while (!Console.KeyAvailable)
        //     {
        //         // tick?
        //     }
		// 	keyInfo = Console.ReadKey(true);

        //     InputManager.OnKey(keyInfo.Key);

		// 	if (keyInfo.Key == ConsoleKey.F1)
		// 	{
        //         // TODO - move to UI action handling
		// 		UIManager.UpdateResolution();
		// 	}
        //     // TODO - make this into a handled action, potentially on the game manager
        //     //Environment.Exit(0);
		// 	UIManager.Draw();
		// 	DisplayManager.Render();
		// }
		// while(keyInfo.Key != ConsoleKey.Escape);
#endregion

#region Engine Loop
        // TODO - gameloop
        //System.Threading.Thread.Sleep(1000);
		while(true)
		{
            // int x = panel1.GetScreenPosition().X + 2;
            // if (x > Console.WindowWidth - panel1.Width)
            // {
            //     x = 0;
            // }
            // panel1.SetLocalPosition(x, panel1.GetScreenPosition().Y+0);            
		    //UIManager.Draw();
            System.Threading.Thread.Sleep(50);
		}
#endregion
			
		return 0;
	}

    static void OnApplicationExit(object sender, EventArgs e)
    {
        OnQuit();
    }

}

