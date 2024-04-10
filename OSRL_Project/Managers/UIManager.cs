

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
    public UILayout Layout;    
    public List<IFocusable> AvailableFocusableCollection = new List<IFocusable>();


    IFocusable CurrentFocusedObject;


#region Initialization
    public override void Awake()
    {
        base.Awake();
        Layout = new UILayout ("Layout", 0, 0, Console.WindowWidth, Console.WindowHeight);
        Layout.CreateLayer(Tags.UILayer_Game);
        Layout.CreateLayer(Tags.UILayer_GameMenu);
        Layout.CreateLayer(Tags.UILayer_Menu);
        Layout.CreateLayer(Tags.UILayer_Modal);
    }
#endregion

    public void UpdateResolution()
    {
        Layout.SetSize(Console.WindowWidth, Console.WindowHeight);
    }

    public void AddUIObject(UIObject objectToAdd, Tag uiLayerTag, bool setToFront = false)
    {
        if (Layout == null)
        {
            Log.Error("Cannot Add UI Object when Layout is Null");
            return;
        }

        Layout.AddUIObject(objectToAdd, uiLayerTag, setToFront);
    }

    public void RemoveUIObject(UIObject objectToRemove)
    {
        if (Layout == null)
        {
            Log.Error("Cannot Remove UI Object when Layout is Null");
            return;
        }

        Layout.RemoveUIObject(objectToRemove);
    }
    
    public void Draw()
    {
        Layout.Draw();
    }

#region Focus
    public void OnSetCanFocus()
    {
        Layout.RecalculateFocus();
    }

    public IFocusable GetCurrentFocusedObject()
    {
        return CurrentFocusedObject;
    }

    public void SetCurrentFocusObject(IFocusable newFocus)
    {
        if (newFocus != null && !newFocus.CanFocus)
        {            
            Log.Warning("Attempting to focus object(" + newFocus.ToString()+ ") with CanFocus==false.");
            return;
        }

        if (CurrentFocusedObject != null)
        {
            CurrentFocusedObject.OnUnfocused();
        }

        CurrentFocusedObject = newFocus;
        if (CurrentFocusedObject != null)
        {
            CurrentFocusedObject.OnFoucsed();
            //Debug.WriteLine("Current focus set to " + CurrentFocusObject.ToString());
        }
        else
        {
            //Debug.WriteLine("Current focus set to NULL");
        }
    }

    public void RecalculateFocusables(UIObject baseFocusable)
    {
        if (baseFocusable == null)
        {
            AvailableFocusableCollection.Clear();
            SetCurrentFocusObject(null);
            return;
        }

        AvailableFocusableCollection = baseFocusable.GetAllFocusables().ToList();

        // TODO - Calc Navigation
        foreach(IFocusable i in AvailableFocusableCollection)
        {
            Rect rect = i.GetScreenSpaceRect();
            IFocusable hitUp    = FocusRaycast(rect.X + ((rect.Width) / 2)  , rect.Y                        , rect.Width , -1, NavigationDirection.up     , new List<IFocusable> { i });
            IFocusable hitDown  = FocusRaycast(rect.X + ((rect.Width) / 2)  , rect.Y + rect.Height-1        , rect.Width , -1, NavigationDirection.down   , new List<IFocusable> { i });
            IFocusable hitLeft  = FocusRaycast(rect.X                       , rect.Y + ((rect.Height) / 2)  , rect.Height, -1, NavigationDirection.left   , new List<IFocusable> { i });
            IFocusable hitRight = FocusRaycast(rect.X + rect.Width-1        , rect.Y + ((rect.Height) / 2)  , rect.Height, -1, NavigationDirection.right  , new List<IFocusable> { i });
            //Log.WriteLine($"{i.Name} + {rect.X} + {rect.Y}");

            i.SetFocusRelation(NavigationDirection.up, hitUp);
            i.SetFocusRelation(NavigationDirection.down, hitDown);
            i.SetFocusRelation(NavigationDirection.left, hitLeft);
            i.SetFocusRelation(NavigationDirection.right, hitRight);

            string upName = hitUp == null       ? "NULL" : hitUp.Name;
            string downName = hitDown == null   ? "NULL" : hitDown.Name;
            string leftName = hitLeft == null   ? "NULL" : hitLeft.Name;
            string rightName = hitRight == null ? "NULL" : hitRight.Name;
            //Log.WriteLine($"SetFocusRelation for {i.Name}: up.{upName} | d.{downName} | l.{leftName} | r.{rightName}");
        }

        SetCurrentFocusObject(baseFocusable.GetFirstFocusable());
    }

    public IFocusable FocusRaycast(int x, int y, int width, int length, NavigationDirection dir, List<IFocusable> toIgnore)
    {
        if (length == -1)
        {
            // TODO - technically fails on massive screens, do I care?
            length = 10000;
        }

        int coordX = x;
        int coordY = y;

        for(int i = 0; i < length; i++)
        {
            foreach (var focusable in AvailableFocusableCollection)
            {
                if (focusable.GetScreenSpaceRect().Contains(coordX, coordY) && !toIgnore.Contains(focusable))
                {
                    //Log.WriteLine($"{toIgnore[0].Name} hit {focusable.Name} at x{coordX}, y{coordY} at length{i} with dir{dir}");
                    return focusable;
                }                
            }
            
            // TODO - implement width

            if (dir == NavigationDirection.up)
            {
                coordY -= 1;
            }
            else if (dir == NavigationDirection.down)
            {
                coordY += 1;
            }
            else if (dir == NavigationDirection.left)
            {
                coordX -= 1;
            }
            else if (dir == NavigationDirection.right)
            {
                coordX += 1;
            }

            if (!Layout.GetScreenSpaceRect().Contains(coordX, coordY))
            {
                // Prevents off-screen gets
                break;
            }
        }
        return null;
    }

    public bool Navigate(NavigationDirection direction)
    {
        IFocusable currentFocus = GetCurrentFocusedObject();
        if (currentFocus == null)
        {
            return false;
        }

        return currentFocus.Navigate(direction);
    }
#endregion

#region Input

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
#endregion
}