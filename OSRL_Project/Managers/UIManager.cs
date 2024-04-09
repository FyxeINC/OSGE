

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

public class UIManager : Singleton<UIManager>
{
    public UIObject Layout;    

    public override void Awake()
    {
        base.Awake();
        Layout = new UIObject ("Layout", 0, 0, Console.WindowWidth, Console.WindowHeight);
    }

    public void UpdateResolution()
    {
        Layout.SetSize(Console.WindowWidth, Console.WindowHeight);
    }

    public List<IFocusable> AvailableFocusableCollection = new List<IFocusable>();

    IFocusable CurrentFocusObject;
    public IFocusable GetCurrentFocusObject()
    {
        return CurrentFocusObject;
    }

    public void SetCurrentFocusObject(IFocusable newFocus)
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

    public UIObject GetCurrentFrontmostObject() 
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

    public bool SetCurrentFrontmostObject(UIObject uiObject)
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

        AvailableFocusableCollection = uiObject.GetAllFocusables();
        UpdateCurrentFrontmostObject();
        
        return true;
    }

    public void UpdateCurrentFrontmostObject()
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

    public bool RegisterUIObject(UIObject uiObject, bool shouldFrontmost = false)
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

    public void Draw()
    {
        Layout.Draw();
    }

    public bool Navigate(NavigationDirection direction)
    {
        IFocusable currentFocus = GetCurrentFocusObject();
        if (currentFocus == null)
        {
            return false;
        }

        return currentFocus.Navigate(direction);
    }

    public void ActionTriggered(InputActionEvent inputActionEvent) 
    {
        if (inputActionEvent.IdentifierTag == Tags.IA_UINavUp)
        {
            // Navigate DOES return if successful/failed, could play sound or fx here
            Navigate(NavigationDirection.up);
        }
        else if (inputActionEvent.IdentifierTag == Tags.IA_UINavDown)
        {
            Navigate(NavigationDirection.down);
        }
        else if (inputActionEvent.IdentifierTag == Tags.IA_UINavLeft)
        {
            Navigate(NavigationDirection.left);
        }
        else if (inputActionEvent.IdentifierTag == Tags.IA_UINavRight)
        {
            Navigate(NavigationDirection.right);
        }
        else if (inputActionEvent.IdentifierTag == Tags.IA_UIGenericBack)
        {
            
        }
        else if (inputActionEvent.IdentifierTag == Tags.IA_UIGenericForward)
        {
            
        }
        else if (inputActionEvent.IdentifierTag == Tags.IA_GeneralQuit)
        {
            // TODO - Move to game management?
            Environment.Exit(0);
        }

        if (inputActionEvent.IdentifierTag.ContainsTag(Tags.IA_CategoryUI))
        {
            inputActionEvent.WasConsumed = true;
        }
    }

    public IFocusable FocusRaycast(int x, int y, int width, NavigationDirection dir, List<IFocusable> toIgnore)
    {
        // TODO 
        return null;
    }
}