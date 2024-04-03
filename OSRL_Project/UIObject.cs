using System.Diagnostics;

public class UIObject : GameObject
{    
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
        foreach (var i in ChildrenCollection)
        {
            if (i is UIObject)
            {
                (i as UIObject).SetIsFrontmost(IsFrontmost);
            }
        }

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
    }

    public virtual void OnFrontmost() {}
    public virtual void OnNotFrontmost() {}

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

    public Rect GetRect()
    {
        Point position = GetScreenPosition();
        int width = Width;
        int height = Height;

        if (UseOffsetTopBottom)
        {
            if (Parent != null)
            {
                height = (Parent as UIObject).GetRect().Height;

                position.Y += OffsetTop;
                height -= OffsetTop;
                height -= OffsetBottom;
            }
        }

        if (UseOffsetLeftRight)
        {
            if (Parent != null)
            {
                width = (Parent as UIObject).GetRect().Width;   
                
                position.X += OffsetLeft;
                width -= OffsetLeft;
                width -= OffsetRight;
            }
        }
        Debug.WriteLine(Name + " | X:" + position.X + " | Y:" + position.Y);
        return new Rect (position, width, height);
    }

    public override void Draw()
    {
        base.Draw();
        //Debug.WriteLine(Name + " --> " + GetRect().ToString());
        //Debug.WriteLine("Drawing: " + Name);
    }

    public virtual void Navigate(NavigationDirection direction)
    {

    }
}