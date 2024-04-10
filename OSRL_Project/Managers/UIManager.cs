

using System.Data;
using System.Diagnostics;
using System.Dynamic;

public enum Direction
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

    public bool RemoveUIObject(UIObject objectToRemove)
    {
        if (Layout == null)
        {
            Log.Error("Cannot Remove UI Object when Layout is Null");
            return false;
        }

        return Layout.RemoveUIObject(objectToRemove);
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
            IFocusable hitUp    = FocusRaycast(rect.X + ((rect.Width) / 2)  , rect.Y                        , rect.Width , -1, Direction.up     , new List<IFocusable> { i });
            IFocusable hitDown  = FocusRaycast(rect.X + ((rect.Width) / 2)  , rect.Y + rect.Height-1        , rect.Width , -1, Direction.down   , new List<IFocusable> { i });
            IFocusable hitLeft  = FocusRaycast(rect.X                       , rect.Y + ((rect.Height) / 2)  , rect.Height, -1, Direction.left   , new List<IFocusable> { i });
            IFocusable hitRight = FocusRaycast(rect.X + rect.Width-1        , rect.Y + ((rect.Height) / 2)  , rect.Height, -1, Direction.right  , new List<IFocusable> { i });
            //Log.WriteLine($"{i.Name} + {rect.X} + {rect.Y}");

            i.SetFocusRelation(Direction.up, hitUp);
            i.SetFocusRelation(Direction.down, hitDown);
            i.SetFocusRelation(Direction.left, hitLeft);
            i.SetFocusRelation(Direction.right, hitRight);

            string upName = hitUp == null       ? "NULL" : hitUp.Name;
            string downName = hitDown == null   ? "NULL" : hitDown.Name;
            string leftName = hitLeft == null   ? "NULL" : hitLeft.Name;
            string rightName = hitRight == null ? "NULL" : hitRight.Name;
            //Log.WriteLine($"SetFocusRelation for {i.Name}: up.{upName} | d.{downName} | l.{leftName} | r.{rightName}");
        }

        SetCurrentFocusObject(baseFocusable.GetFirstFocusable());
    }

    public IFocusable FocusRaycast(int x, int y, int width, int length, Direction dir, List<IFocusable> toIgnore)
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
            
            // TODO - implement width

            // for(int j = 0; j < width; j++)
            // {
            //     int currentCoordX = coordX;
            //     int currentCoordY = coordY;
                
            //     if (dir == NavigationDirection.up)
            //     {
            //         currentCoordX = coordX - (width - j);
            //     }


            //     foreach (var focusable in AvailableFocusableCollection)
            //     {
            //         if (focusable.GetScreenSpaceRect().Contains(currentCoordX, currentCoordY) && !toIgnore.Contains(focusable))
            //         {
            //             //Log.WriteLine($"{toIgnore[0].Name} hit {focusable.Name} at x{coordX}, y{coordY} at length{i} with dir{dir}");
            //             return focusable;
            //         }                
            //     }
            // }

            foreach (var focusable in AvailableFocusableCollection)
            {
                if (focusable.GetScreenSpaceRect().Contains(coordX, coordY) && !toIgnore.Contains(focusable))
                {
                    //Log.WriteLine($"{toIgnore[0].Name} hit {focusable.Name} at x{coordX}, y{coordY} at length{i} with dir{dir}");
                    return focusable;
                }                
            }
            

            if (dir == Direction.up)
            {
                coordY -= 1;
            }
            else if (dir == Direction.down)
            {
                coordY += 1;
            }
            else if (dir == Direction.left)
            {
                coordX -= 1;
            }
            else if (dir == Direction.right)
            {
                coordX += 1;
            }

            if (!Layout.GetScreenSpaceRect().Contains(coordX, coordY))
            {
                // Prevents off-screen checks
                break;
            }
        }
        return null;
    }

    public bool Navigate(Direction direction)
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
            Navigate(Direction.up);
        }
        else if (inputActionEvent.IdentifierTag == Tags.IA_UINavDown)
        {
            Navigate(Direction.down);
        }
        else if (inputActionEvent.IdentifierTag == Tags.IA_UINavLeft)
        {
            Navigate(Direction.left);
        }
        else if (inputActionEvent.IdentifierTag == Tags.IA_UINavRight)
        {
            Navigate(Direction.right);
        }
        else if (inputActionEvent.IdentifierTag == Tags.IA_UIGenericBack)
        {
            if (!Layout.HandleBackAction())
            {
                // Idk, a noise?
            }
        }
        else if (inputActionEvent.IdentifierTag == Tags.IA_UIGenericForward)
        {
            IFocusable currentFocus = GetCurrentFocusedObject();
            if (currentFocus != null)
            {
                currentFocus.OnInteract();
            }
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