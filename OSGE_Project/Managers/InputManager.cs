public class InputActionEvent
{
    public InputActionEvent(InputAction inputAction)
    {
        EventAction = inputAction;
    }
    public InputAction EventAction;
    public Tag IdentifierTag {get {return EventAction.Identifier;}}
    public bool WasConsumed = false;
}

public class InputManager : Singleton<InputManager>
{
    public string SaveDataFileName = "SaveData_Input";
    public SaveData_Input CurrentSaveData;

    // TODO - create sorting list and order for IMCs to allow for precedent inputs
    public List<InputMappingContext> ActiveContextCollection = new List<InputMappingContext> ();
    public Dictionary<Tag, InputMappingContext> AllContextCollection = new Dictionary<Tag, InputMappingContext> ();

    public List<IInputHandler> InputHandlerCollection = new List<IInputHandler> ();
        
    public override void Start()
    {
        base.Start();
        // DEBUG
        ActiveContextCollection.Add(InputMappingContexts.UI_Default);

        PlayerProfileManager.instance.OnCurrentPlayerProfileChanged += OnPlayerProfileChanged;
        OnPlayerProfileChanged(PlayerProfileManager.instance.CurrentPlayerProfile);

        Thread thread = new Thread(() => 
        {
            
		    ConsoleKeyInfo keyInfo = new ConsoleKeyInfo ();
            do
            {
                while (!Console.KeyAvailable)
                {
                    // tick?
                    //System.Threading.Thread.Sleep(250);
                }
                keyInfo = Console.ReadKey(true);
                //Log.WriteLine("Key Pressed: " + keyInfo.KeyChar);
                InputManager.instance.OnKey(keyInfo.Key);
            }
            while(true);
        });
        thread.IsBackground = true;
        thread.Start();
    }

    public void OnPlayerProfileChanged(PlayerProfile newPlayerProfile)
    {
        CurrentSaveData = SaveManager.instance.LoadFromFile<SaveData_Input>(SaveDataFileName);
        if (CurrentSaveData == null)
        {
            CurrentSaveData = new SaveData_Input ();
            // TODO - Create default keybindings
            if (!SaveManager.instance.SaveToFile<SaveData_Input>(CurrentSaveData, SaveDataFileName))
            {
                Log.Error($"Unable to save input settings to file. Filename({SaveDataFileName})");
            }
        }
        LoadKeybindings(CurrentSaveData.KeyBindingCollection);
    }

    public void OnDataSaved()
    {
        // DEBUG to test saving
        if (!AllContextCollection.ContainsKey(InputMappingContexts.UI_Default.MapTag))
        {
            AllContextCollection.Add(InputMappingContexts.UI_Default.MapTag, InputMappingContexts.UI_Default);
        }
        
        SaveKeybindings();

        SaveManager.instance.SaveToFile<SaveData_Input>(CurrentSaveData, SaveDataFileName);
    }

    public void LoadKeybindings(List<KeyValuePair<Tag, List<KeyValuePair<Tag, List<ConsoleKey>>>>> savedBindings)
    {
        foreach (KeyValuePair<Tag, List<KeyValuePair<Tag, List<ConsoleKey>>>> i in savedBindings)
        {
            Tag imcTag = i.Key;
            if (!AllContextCollection.ContainsKey(imcTag))
            {
                AllContextCollection.Add(imcTag, new InputMappingContext (imcTag));
            }

            foreach (KeyValuePair<Tag, List<ConsoleKey>> j in i.Value)
            {
                Tag actionTag = j.Key;
                if (!AllContextCollection[imcTag].MappedActions.ContainsKey(actionTag))
                {
                    AllContextCollection[imcTag].MappedActions.Add(actionTag, new InputActionMapping());
                }

                foreach (ConsoleKey k in j.Value)
                {
                    if (!AllContextCollection[imcTag].MappedActions[actionTag].MappedKeys.Contains(k))
                    {
                        AllContextCollection[imcTag].MappedActions[actionTag].MappedKeys.Add(k);
                    }
                }
            }   
        }
    }

    public void SaveKeybindings()
    {
        List<KeyValuePair<Tag, List<KeyValuePair<Tag, List<ConsoleKey>>>>> kbc = new List<KeyValuePair<Tag, List<KeyValuePair<Tag, List<ConsoleKey>>>>> ();

        foreach(InputMappingContext imc in AllContextCollection.Values)
        {
            KeyValuePair<Tag, List<KeyValuePair<Tag, List<ConsoleKey>>>> addedIMC = new KeyValuePair<Tag, List<KeyValuePair<Tag, List<ConsoleKey>>>> (imc.MapTag, new List<KeyValuePair<Tag, List<ConsoleKey>>> ());            

            foreach (KeyValuePair<Tag, InputActionMapping> action in imc.MappedActions)
            {
                KeyValuePair<Tag, List<ConsoleKey>> addedAction = new KeyValuePair<Tag, List<ConsoleKey>> (action.Key, new List<ConsoleKey> ());                                

                foreach (ConsoleKey cKey in action.Value.MappedKeys)
                {
                    addedAction.Value.Add(cKey);
                }

                addedIMC.Value.Add(addedAction);
            }

            kbc.Add(addedIMC);
        }

        CurrentSaveData.KeyBindingCollection = kbc;
    }

    public void EnableMappingContext(Tag imcTag)
    {
        if (!AllContextCollection.ContainsKey(imcTag))
        {
            return;
        }

        if (ActiveContextCollection.Contains(AllContextCollection[imcTag]))
        {
            return;
        }

        ActiveContextCollection.Add(AllContextCollection[imcTag]);
    }

    public void RemoveMappingContext(Tag imcTag)
    {
        if (!AllContextCollection.ContainsKey(imcTag))
        {
            return;
        }

        if (!ActiveContextCollection.Contains(AllContextCollection[imcTag]))
        {
            return;
        }

        ActiveContextCollection.Remove(AllContextCollection[imcTag]);
    }

    public void OnKey(ConsoleKey key)
    {
        List<InputAction> inputActions = new List<InputAction> ();
        foreach (var i in ActiveContextCollection)
        {
            inputActions.AddRange(i.GetActionForKey(key));
        }

        foreach (var ia in inputActions)
        {
            InputActionEvent actionEvent = new InputActionEvent (ia);
            
            // TODO - find a way to make this a part of IHCollection
            UIManager.instance.ActionTriggered(actionEvent);
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