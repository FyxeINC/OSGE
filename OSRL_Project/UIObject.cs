using System.Data.SqlTypes;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters;

public enum AnchorPointHorizonal
{
    left, 
    center, 
    right, 
    stretch
}
public enum AnchorPointVertical
{
    bottom, 
    middle, 
    top, 
    stretch    
}

public class AnchorPoint
{
    public AnchorPointHorizonal Horizontal;
    public AnchorPointVertical Vertical;

    public AnchorPoint() 
    {
        Horizontal = AnchorPointHorizonal.left;
        Vertical = AnchorPointVertical.top;
    }

    public AnchorPoint(AnchorPointHorizonal horizonal, AnchorPointVertical anchorPointVertical)
    {
        Horizontal = horizonal;
        Vertical = anchorPointVertical;
    }
}

public class UIObject : GameObject, IFocusable
{    
    public virtual bool CanFocus {get; set;}
    public bool IsFocused 
    {
        get 
        {
            return UIManager.GetCurrentFocusObject() == this;
        }
    }
    
#region Constructors
    public UIObject(string name, Point screenPosition, int width, int height,
        ConsoleColor foreground = ConsoleColor.White, 
        ConsoleColor background = ConsoleColor.DarkGray, 
        ConsoleColor foregroundNotFrontmost = ConsoleColor.Gray, 
        ConsoleColor backgroundNotFrontmost = ConsoleColor.Black) 
        : base(name, screenPosition)
    {
        Width = width;
        Height = height;
        ColorForegroundFrontmost = foreground;
        ColorBackgroundFrontmost = background;
        ColorForegroundNotFrontmost = foregroundNotFrontmost;
        ColorBackgroundNotFrontmost = backgroundNotFrontmost;
    }

    public UIObject(string name, int x, int y, int width, int height,
        ConsoleColor foreground = ConsoleColor.White, 
        ConsoleColor background = ConsoleColor.DarkGray, 
        ConsoleColor foregroundNotFrontmost = ConsoleColor.Gray, 
        ConsoleColor backgroundNotFrontmost = ConsoleColor.Black) 
        : base(name, x, y)
    {
        Width = width;
        Height = height;
        ColorForegroundFrontmost = foreground;
        ColorBackgroundFrontmost = background;
        ColorForegroundNotFrontmost = foregroundNotFrontmost;
        ColorBackgroundNotFrontmost = backgroundNotFrontmost;
    }

    // TODO - constructor for fill/stretch
#endregion

    public AnchorPoint CurrentAnchorPoint = new AnchorPoint ();
    public int Width;
    public int Height;
    public bool UseOffsetTopBottom = false;
    public bool UseOffsetLeftRight = false;
    public int OffsetTop;
    public int OffsetBottom;
    public int OffsetLeft;
    public int OffsetRight;

    public bool IsFrontmost;
    
    public ConsoleColor ColorForegroundFrontmost {get; set;}
    public ConsoleColor ColorForegroundNotFrontmost {get; set;}
    public ConsoleColor ColorBackgroundFrontmost {get; set;}
    public ConsoleColor ColorBackgroundNotFrontmost {get; set;}
    
    public Dictionary<NavigationDirection, IFocusable> FocusCollection {get; set;} = new Dictionary<NavigationDirection, IFocusable> ();

    public void SetAnchorPoint(AnchorPointHorizonal horizontal, AnchorPointVertical vertical)
    {
        CurrentAnchorPoint = new AnchorPoint (horizontal, vertical);
    }

    public void SetSize(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void SetOffset(int top, int bottom, int left, int right)
    {
        OffsetTop = top;
        OffsetBottom = bottom;
        OffsetLeft = left;
        OffsetRight = right;        
    }

    public override ConsoleColor GetColorForeground()
    {
        if (IsFrontmost)
        {
            return ColorForegroundFrontmost;
        }
        else
        {
            return ColorForegroundNotFrontmost;
        }
    }

    public override ConsoleColor GetColorBackground()
    {
        if (IsFrontmost)
        {
            return ColorBackgroundFrontmost;
        }
        else
        {
            return ColorBackgroundNotFrontmost;
        }
    }

    public void SetIsFrontmost(bool isFrontmost)
    {
        bool hasChanged = IsFrontmost != isFrontmost;

        IsFrontmost = isFrontmost;
        if (hasChanged)
        {
            if (IsFrontmost)
            {
                OnFrontmost();
            }
            else
            {
                OnNotFrontmost();
            }
        } 

        foreach (var i in ChildrenCollection)
        {
            if (i is UIObject)
            {
                (i as UIObject).SetIsFrontmost(IsFrontmost);
            }
        }       
    }
    public virtual void OnFrontmost() {}
    public virtual void OnNotFrontmost() {}

#region Focus
    public IFocusable GetFirstFocusable()
    {
        if (this.CanFocus)
        {
            return this as IFocusable;
        }

        IFocusable foundFocusable = null;
        foreach (var i in ChildrenCollection)
        {
            foundFocusable = (i as UIObject).GetFirstFocusable();
            if (foundFocusable != null)
            {
                break;
            }
        }
        return foundFocusable;
    }
    public virtual void OnFoucsed(){}
    public virtual void OnUnfocused(){}

    public virtual bool Navigate(NavigationDirection direction)
    {
        if (!FocusCollection.ContainsKey(direction))
        {
            return false;
        }

        UIManager.SetCurrentFocusObject(FocusCollection[direction]);
        return true;
    }

    /// <summary>
    /// Sets a focus relation on this object to another, given a relation. 
    /// If NULL is provided, it will remove the focus relation.
    /// </summary>
    public void SetFocusRelation(IFocusable focusable, NavigationDirection direction)
    {
        if (focusable == null)
        {
            FocusCollection.Remove(direction);
            return;
        }

        if (FocusCollection.ContainsKey(direction))
        {
            FocusCollection[direction] = focusable;
        }
        else
        {
            FocusCollection.Add(direction, focusable);
        }
    }
#endregion

    public override void SetParent(GameObject newParent)
    {
        base.SetParent(newParent);
        if (newParent is UIObject)
        {
            SetIsFrontmost((newParent as UIObject).IsFrontmost);
        }
    }

    public override Point GetScreenPosition()
    {
        if (!UseOffsetLeftRight && !UseOffsetTopBottom)
        {
            return base.GetScreenPosition();
        }
        Point newPoint = base.GetScreenPosition();
        if (UseOffsetLeftRight)
        {
            newPoint.X += OffsetLeft;
        }
        if (UseOffsetTopBottom)
        {
            newPoint.Y += OffsetTop;
        }
        return newPoint;
    }

    // public Rect GetRect()
    // {
    //     Point position = GetScreenPosition();
    //     int width = Width;
    //     int height = Height;        

    //     if (UseOffsetTopBottom)
    //     {
    //         if (Parent != null)
    //         {
    //             height = (Parent as UIObject).GetRect().Height;

    //             position.Y += OffsetTop;
    //             height -= OffsetTop;
    //             height -= OffsetBottom;
    //         }
    //     }

    //     if (UseOffsetLeftRight)
    //     {
    //         if (Parent != null)
    //         {
    //             width = (Parent as UIObject).GetRect().Width;   
                
    //             position.X += OffsetLeft;
    //             width -= OffsetLeft;
    //             width -= OffsetRight;
    //         }
    //     }
    //     //Debug.WriteLine(Name + " | X:" + position.X + " | Y:" + position.Y);
    //     return new Rect (position, width, height);
    // }

    public Rect GetRect()
    {
        Rect parentRect = new Rect();
        if (Parent != null)
        {
            parentRect = (Parent as UIObject).GetRect();
        }
        int x = 0;
        int y = 0;
        int width = 0;
        int height = 0;

        switch(CurrentAnchorPoint.Horizontal)
        {
            default:
            case AnchorPointHorizonal.left:
                x = parentRect.X + ScreenPosition.X;
                width = Width;
                break;        
            case AnchorPointHorizonal.center:
                x = parentRect.X + (parentRect.Width / 2) + ScreenPosition.X - (Width / 2);
                width = Width;
                break;
            case AnchorPointHorizonal.right:
                x = parentRect.X + parentRect.Width + ScreenPosition.X - Width;
                width = Width;
                break;
            case AnchorPointHorizonal.stretch:
                x = parentRect.X + OffsetLeft;
                width = parentRect.Width;
                width -= OffsetLeft;
                width -= OffsetRight;
                break;
        }
        switch(CurrentAnchorPoint.Vertical)
        {
            default:
            case AnchorPointVertical.top:
                y = parentRect.Y + ScreenPosition.Y;
                height = Height;
                break;        
            case AnchorPointVertical.middle:
                y = parentRect.Y + (parentRect.Height / 2) + ScreenPosition.Y - (Height / 2);
                height = Height;
                break;
            case AnchorPointVertical.bottom:
                y = parentRect.Y + parentRect.Height + ScreenPosition.Y - Height;
                height = Height;
                break;
            case AnchorPointVertical.stretch:
                y = parentRect.Y + OffsetTop;
                height = parentRect.Height;
                height -= OffsetTop;
                height -= OffsetBottom;
                break;
        }

        return new Rect(x, y, width, height);
    }

    public override void Draw()
    {
        base.Draw();
        //Debug.WriteLine(Name + " --> " + GetRect().ToString());
        //Debug.WriteLine("Drawing: " + Name);
    }


}