

using System.Data;
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

    public static IFocusable GetCurrentFocusObject()
    {
        return null;
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
    }

    public static bool RegisterUIObject(UIObject uiObject, bool shouldFrontmost = false)
    {
        if (Layout == null)
        {
            return false;
        }
        if (Layout.ContainsChild(uiObject))
        {
            return false;
        }

        Layout.AddChild(uiObject);

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
            return;
        }

        Layout.RemoveChild(uiObject);          
        UpdateCurrentFrontmostObject();              
    }

    public static void Draw()
    {
        Layout.Draw();
    }

    public static void Navigate(NavigationDirection direction)
    {
        UIObject currentFrontmost = GetCurrentFrontmostObject();
        if (currentFrontmost == null)
        {
            return;
        }

        currentFrontmost.Navigate(direction);
    }
}