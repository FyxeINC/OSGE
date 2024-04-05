

using System.Data;
using System.Diagnostics;
using System.Dynamic;

public enum NavigationDirection
{
    up,
    down,
    left,
    right
}

public static class UIManager
{
    public static UIObject Layout;    

    public static void Initialize()
    {
        Layout = new UIObject ("Layout", 0, 0, Console.WindowWidth, Console.WindowHeight);
    }

    public static void UpdateResolution()
    {
        Layout.SetSize(Console.WindowWidth, Console.WindowHeight);
    }

    static IFocusable CurrentFocusObject;
    public static IFocusable GetCurrentFocusObject()
    {
        return CurrentFocusObject;
    }

    public static void SetCurrentFocusObject(IFocusable newFocus)
    {
        if (newFocus != null && !newFocus.CanFocus)
        {            
            Log.Warning("Attempting to focus object(" + newFocus.ToString()+ ") with CanFocus==false.");
            return;
        }

        if (CurrentFocusObject != null)
        {
            CurrentFocusObject.OnUnfocused();
        }

        CurrentFocusObject = newFocus;
        if (CurrentFocusObject != null)
        {
            CurrentFocusObject.OnFoucsed();
            //Debug.WriteLine("Current focus set to " + CurrentFocusObject.ToString());
        }
        else
        {
            //Debug.WriteLine("Current focus set to NULL");
        }
    }

    public static UIObject GetCurrentFrontmostObject() 
    { 
        if (Layout == null)
        {
            return null;
        }
        if (Layout.GetChildrenCount() <= 0)
        {
            return null;
        }
        else 
        {
            return Layout.GetChildren()[0] as UIObject;
        }
    }

    public static bool SetCurrentFrontmostObject(UIObject uiObject)
    {
        if (Layout == null)
        {
            Log.Error("Cannot Set Frontmost Object when Layout is Null");
            return false;
        }

        int index = Layout.GetChildIndex(uiObject);
        if (index == -1)
        {
            return false;
        }
        else if (index == 0)
        {
            return false;
        }

        Debug.WriteLine("Current frontmost object set to " + uiObject.Name);
        Layout.SetChildIndex(uiObject, 0);

        UpdateCurrentFrontmostObject();
        
        return true;
    }

    public static void UpdateCurrentFrontmostObject()
    {
        for (int i = 0; i < Layout.GetChildrenCount(); i++)
        {
            (Layout.GetChildren()[i] as UIObject).SetIsFrontmost(i == 0);
        }

        // Get focused object
        UIObject frontmost = GetCurrentFrontmostObject();
        if (frontmost != null)
        {
            SetCurrentFocusObject(GetCurrentFrontmostObject().GetFirstFocusable());
        }
        else
        {
            SetCurrentFocusObject(null);
        }
    }

    public static bool RegisterUIObject(UIObject uiObject, bool shouldFrontmost = false)
    {
        if (Layout == null)
        {
            Log.Error("Cannot Register UI Object when Layout is Null");
            return false;
        }
        if (Layout.ContainsChild(uiObject))
        {
            return false;
        }

        Layout.AddChild(uiObject, false);

        if (shouldFrontmost)
        {
            // Calls update frontmost object
            SetCurrentFrontmostObject(uiObject);
        }
        else
        {
            UpdateCurrentFrontmostObject();
        }

        return true;
    }

    public static void DestroyUIObject(this UIObject uiObject)
    {
        if (Layout == null)
        {
            Log.Error("Cannot Destroy UI Object when Layout is Null");
            return;
        }

        Layout.RemoveChild(uiObject);          
        UpdateCurrentFrontmostObject();              
    }

    public static void Draw()
    {
        Layout.Draw();
    }

    public static bool Navigate(NavigationDirection direction)
    {
        IFocusable currentFocus = GetCurrentFocusObject();
        if (currentFocus == null)
        {
            return false;
        }

        return currentFocus.Navigate(direction);
    }

    public static void ActionTriggered(InputActionEvent inputActionEvent) 
    {
        if (inputActionEvent.EventAction == InputActions.UI_Navigate_Up)
        {
            // Navigate DOES return if successful/failed, could play sound or fx here
            Navigate(NavigationDirection.up);
            inputActionEvent.WasConsumed = true;
        }
        else if (inputActionEvent.EventAction == InputActions.UI_Navigate_Down)
        {
            Navigate(NavigationDirection.down);
            inputActionEvent.WasConsumed = true;
        }
        else if (inputActionEvent.EventAction == InputActions.UI_Navigate_Left)
        {
            Navigate(NavigationDirection.left);
            inputActionEvent.WasConsumed = true;
        }
        else if (inputActionEvent.EventAction == InputActions.UI_Navigate_Right)
        {
            Navigate(NavigationDirection.right);
            inputActionEvent.WasConsumed = true;
        }
    }
}