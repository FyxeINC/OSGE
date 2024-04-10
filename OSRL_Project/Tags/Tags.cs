public static class Tags
{
#region InputAction
    // Categories
    public static Tag IA_CategoryUI = new Tag ("InputAction.UI");
    public static Tag IA_CategoryGeneral = new Tag ("InputAction.General");

    // Actions

    // UI
    public static Tag IA_UINavUp = new Tag ("InputAction.UI.NavigateUp");
    public static Tag IA_UINavDown = new Tag ("InputAction.UI.NavigateDown");
    public static Tag IA_UINavLeft = new Tag ("InputAction.UI.NavigateLeft");
    public static Tag IA_UINavRight = new Tag ("InputAction.UI.NavigateRight");
    public static Tag IA_UIGenericBack = new Tag ("InputAction.UI.GenericBack");
    public static Tag IA_UIGenericForward = new Tag ("InputAction.UI.GenericForward");

    // General
    public static Tag IA_GeneralQuit = new Tag ("InputAction.General.Quit");
#endregion

#region Localization
    // Languages
    public static Tag Lang_EN = new Tag ("en");
    public static Tag Lang_ES = new Tag ("es");
    public static Tag Lang_FR = new Tag ("fr");
    
    // Strings
    public static Tag Loc_Yes = new Tag ("Localization.Yes");
    public static Tag Loc_No = new Tag ("Localization.No");
    public static Tag Loc_Quit = new Tag ("Localization.Quit");
    public static Tag Loc_Settings = new Tag ("Localization.Settings");
    public static Tag Loc_Play = new Tag ("Localization.Play");
    public static Tag Loc_PressAnyKey = new Tag ("Localization.PressAnyKey");
    public static Tag Loc_Panel = new Tag ("Localization.Panel");
    
#endregion

#region UI
    /// <summary>
    /// E.g. HUD elements
    /// </summary>
    public static Tag UILayer_Game = new Tag ("UI.Layer.Game");
    /// <summary>
    /// E.g. Inventories, dialogue
    /// </summary>
    public static Tag UILayer_GameMenu = new Tag ("UI.Layer.GameMenu");
    /// <summary>
    /// E.g. Pause, settings, main menu
    /// </summary>
    public static Tag UILayer_Menu = new Tag ("UI.Layer.Menu");
    /// <summary>
    /// E.g. Yes/No
    /// </summary>
    public static Tag UILayer_Modal = new Tag ("UI.Layer.Modal");
#endregion
}