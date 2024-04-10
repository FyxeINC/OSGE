using System.Reflection;

public static class InputActions
{
#region UI
    public static InputAction UI_Navigate_Up        = new InputAction (Tags.IA_CategoryUI, Tags.IA_UINavUp, "Navigate Up", "Navigates the UI upwards.");
    public static InputAction UI_Navigate_Down      = new InputAction (Tags.IA_CategoryUI, Tags.IA_UINavDown, "Navigate Down", "Navigates the UI downwards.");
    public static InputAction UI_Navigate_Left      = new InputAction (Tags.IA_CategoryUI, Tags.IA_UINavLeft, "Navigate Left", "Navigates the UI left.");
    public static InputAction UI_Navigate_Right     = new InputAction (Tags.IA_CategoryUI, Tags.IA_UINavRight, "Navigate Right", "Navigates the UI right.");
    public static InputAction UI_Generic_Back       = new InputAction (Tags.IA_CategoryUI, Tags.IA_UIGenericBack, "Generic Back", "");
    public static InputAction UI_Generic_Forward    = new InputAction (Tags.IA_CategoryUI, Tags.IA_UIGenericForward, "Generic Forward", "");
#endregion

#region General
    public static InputAction General_Quit = new InputAction (Tags.IA_CategoryGeneral, Tags.IA_GeneralQuit,"Quit", "");
#endregion

    public static InputAction GetActionForTag(Tag identifier)
    {
        BindingFlags bindingFlags = BindingFlags.Public |
                            BindingFlags.NonPublic |
                            BindingFlags.Instance |
                            BindingFlags.Static;

        foreach (FieldInfo field in typeof(InputActions).GetFields(bindingFlags))
        {
            object value = field.GetValue(null);
            if (value is InputAction && (value as InputAction).Identifier == identifier)
            {
                return value as InputAction;
            }
        }
        return null;
    }
}

public static class InputMappingContexts
{
    #region UI
    public static InputMappingContext UI_Default = new InputMappingContext (new Tag ("UI.Default"), 
        new Dictionary<Tag, InputActionMapping> {
            {Tags.IA_UINavUp, new InputActionMapping (new List<ConsoleKey> { ConsoleKey.UpArrow, ConsoleKey.W})},
            {Tags.IA_UINavDown, new InputActionMapping (new List<ConsoleKey> { ConsoleKey.DownArrow, ConsoleKey.S})},
            {Tags.IA_UINavLeft, new InputActionMapping (new List<ConsoleKey> { ConsoleKey.LeftArrow, ConsoleKey.A})},
            {Tags.IA_UINavRight, new InputActionMapping (new List<ConsoleKey> { ConsoleKey.RightArrow, ConsoleKey.D})},
            {Tags.IA_UIGenericBack, new InputActionMapping (new List<ConsoleKey> { ConsoleKey.Escape})},
            {Tags.IA_UIGenericForward, new InputActionMapping (new List<ConsoleKey> { ConsoleKey.Enter})},
            {Tags.IA_GeneralQuit, new InputActionMapping (new List<ConsoleKey> { ConsoleKey.F1})}
        } );
    #endregion
}