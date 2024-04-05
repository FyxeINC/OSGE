public class InputActionEvent
{
    public InputActionEvent(InputAction inputAction)
    {
        EventAction = inputAction;
    }
    public InputAction EventAction;
    public Tag Identifier {get {return EventAction.Identifier;}}
    public bool WasConsumed = false;
}

public static class InputManager
{
    public static List<InputMappingContext> ActiveContextCollection = new List<InputMappingContext> ();

    public static List<IInputHandler> InputHandlerCollection = new List<IInputHandler> ();
        
    public static void Initialize()
    {
        // DEBUG
        ActiveContextCollection.Add(InputMappingContexts.UI_Default);

        Thread thread = new Thread(() => 
        {
            
		    ConsoleKeyInfo keyInfo = new ConsoleKeyInfo ();
            do
            {
                while (!Console.KeyAvailable)
                {
                    // tick?
                    System.Threading.Thread.Sleep(250);
                }
                keyInfo = Console.ReadKey(true);
                Log.WriteLine("Key Pressed: " + keyInfo.KeyChar);
                InputManager.OnKey(keyInfo.Key);

                if (keyInfo.Key == ConsoleKey.F1)
                {
                    // TODO - move to UI action handling
                    UIManager.UpdateResolution();
                }
                // TODO - make this into a handled action, potentially on the game manager
                //Environment.Exit(0);
            }
            while(true);
        });
        thread.IsBackground = true;
        thread.Start();
    }

    public static void RegisterHandler(this IInputHandler handler)
    {
        if (!InputHandlerCollection.Contains(handler))
        {
            InputHandlerCollection.Add(handler);
        }
    }

    public static void UnregisterHandler(this IInputHandler handler)
    {
        InputHandlerCollection.Remove(handler);
    }

    public static void OnKey(ConsoleKey key)
    {
        List<InputAction> inputActions = new List<InputAction> ();
        foreach (var i in ActiveContextCollection)
        {
            inputActions.AddRange(i.GetActionForKey(key));
        }

        foreach (var ia in inputActions)
        {
            InputActionEvent actionEvent = new InputActionEvent (ia);
            
            UIManager.ActionTriggered(actionEvent);
            if (actionEvent.WasConsumed)
            {
                continue;
            }

            foreach(var ih in InputHandlerCollection)
            {
                ih.ActionTriggered(actionEvent);
                if (actionEvent.WasConsumed)
                {
                    continue;
                }
            }
        }    
    }
}