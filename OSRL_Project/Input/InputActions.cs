public static class inputActions
{
#region UI
    public static Tag CategoryUI = new Tag ("UI");
    public static InputAction UI_Navigate_Up = new InputAction (CategoryUI,"Navigate Up", "Navigates the UI upwards.");
    public static InputAction UI_Navigate_Down = new InputAction (CategoryUI,"Navigate Down", "Navigates the UI downwards.");
    public static InputAction UI_Navigate_Left = new InputAction (CategoryUI,"Navigate Left", "Navigates the UI left.");
    public static InputAction UI_Navigate_Right = new InputAction (CategoryUI,"Navigate Right", "Navigates the UI right.");
    public static InputAction UI_Generic_Back = new InputAction (CategoryUI,"Generic Back", "");
    public static InputAction UI_Generic_Forwards = new InputAction (CategoryUI,"Generic Forward", "");
#endregion

#region General
    public static Tag CategoryGeneral = new Tag ("General");
    public static InputAction General_Quit = new InputAction (CategoryGeneral,"Quit", "");
    public static InputAction General_Pause = new InputAction (CategoryGeneral,"Pause", "");
#endregion

}