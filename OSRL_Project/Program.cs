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
		Console.Title = "OSRL - (F1 to Quit)";
		
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
        backgroundFill.SetColors(ConsoleColor.DarkGray, ConsoleColor.DarkGreen);

		UI_Panel panel1 = new UI_Panel ("panel1", 0, 0, 20, 10, BorderType.doubleLine, "PANEL 1");
        panel1.SetColors(ConsoleColor.White, ConsoleColor.Black);
		panel1.SetAnchorPoint(AnchorPointHorizonal.left, AnchorPointVertical.top);				
		
        UI_Bar barA = new UI_Bar (1,1,0,8);
        barA.SetAnchorPoint(AnchorPointHorizonal.stretch, AnchorPointVertical.top);
        barA.SetOffset(1,1,1,1);
        barA.SetColors(ConsoleColor.Red, ConsoleColor.Gray);
        barA.SetFillDirection(Direction.down);
        barA.SetFillPercentage(0.25f);
        //barA.SetCanFocus(true);

        panel1.AddChild(barA, false);  

		UIManager.instance.AddUIObject(backgroundFill, Tags.UILayer_Game);
		UIManager.instance.AddUIObject(panel1, Tags.UILayer_Game);
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
			
		return 0;
#endregion
	}

    static void OnApplicationExit(object sender, EventArgs e)
    {
        OnQuit();
    }
}

