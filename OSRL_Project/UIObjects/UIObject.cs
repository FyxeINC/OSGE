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
	top,
	middle,
	bottom,
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
#region Focus
	public virtual bool CanFocus { get; set; }
	public bool IsFocused
	{
		get
		{
			return UIManager.instance.GetCurrentFocusObject() == this;
		}
	}
#endregion

#region Constructors
	public UIObject(string name, Point screenPosition, int width, int height)
		: base(name, screenPosition)
	{
		Width = width;
		Height = height;
	}

	public UIObject(string name, int x, int y, int width, int height)
		: base(name, x, y)
	{
		Width = width;
		Height = height;
	}

	// TODO - constructor for fill/stretch
#endregion

#region Colors
	public ConsoleColor ColorForegroundFrontmostFocused { get; set; }
	public ConsoleColor ColorForegroundFrontmostNotFocused { get; set; }
	public ConsoleColor ColorForegroundNotFrontmost { get; set; }
	public ConsoleColor? ColorBackgroundFrontmostFocused { get; set; } = null;
	public ConsoleColor? ColorBackgroundFrontmostNotFocused { get; set; } = null;
	public ConsoleColor? ColorBackgroundNotFrontmost { get; set; } = null;
#endregion

	public AnchorPoint CurrentAnchorPoint = new AnchorPoint();
	public int Width;
	public int Height;
	public int OffsetTop;
	public int OffsetBottom;
	public int OffsetLeft;
	public int OffsetRight;

	public bool IsFrontmost;

	public Dictionary<NavigationDirection, IFocusable> FocusCollection { get; set; } = new Dictionary<NavigationDirection, IFocusable>();    
    
    public virtual void SetColors(ConsoleColor foreground, ConsoleColor? background)
    {
        SetColors(foreground, foreground, foreground, background, background, background);
    }

	public virtual void SetColors(
		ConsoleColor foregroundFrontmostFocused,
		ConsoleColor foregroundFrontmostNotFocused,
		ConsoleColor foregroundNotFrontmost,
		ConsoleColor? backgroundFrontmostFocused,
		ConsoleColor? backgroundFrontmostNotFocused,
		ConsoleColor? backgroundNotFrontmost
		)
	{
		ColorForegroundFrontmostFocused = foregroundFrontmostFocused;
		ColorForegroundFrontmostNotFocused = foregroundFrontmostNotFocused;
		ColorForegroundNotFrontmost = foregroundNotFrontmost;
        if (backgroundFrontmostFocused.HasValue)
        {
		    ColorBackgroundFrontmostFocused = backgroundFrontmostFocused.Value;
        }
        else
        {
            ColorBackgroundFrontmostFocused = null;
        }
        
        if (backgroundFrontmostNotFocused.HasValue)
        {
		    ColorBackgroundFrontmostNotFocused = backgroundFrontmostNotFocused.Value;
        }
        else
        {
            ColorBackgroundFrontmostNotFocused = null;
        }

        
        if (backgroundFrontmostNotFocused.HasValue)
        {
		    ColorBackgroundNotFrontmost = backgroundNotFrontmost.Value;
        }
        else
        {
		    ColorBackgroundNotFrontmost = null;
        }
	}

	public void SetAnchorPoint(AnchorPointHorizonal horizontal, AnchorPointVertical vertical)
	{
		CurrentAnchorPoint = new AnchorPoint(horizontal, vertical);
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
			if (IsFocused)
			{
				return ColorForegroundFrontmostFocused;
			}
			else
			{
				return ColorForegroundFrontmostNotFocused;
			}
		}
		else
		{
			return ColorForegroundNotFrontmost;
		}
	}

	public override ConsoleColor? GetColorBackground()
	{
		if (IsFrontmost)
		{
			if (IsFocused)
			{
                if (ColorBackgroundFrontmostFocused.HasValue)
                {
				    return ColorBackgroundFrontmostFocused.Value;
                }
                else
                {
                    return null;
                }
			}
			else
			{
                if (ColorBackgroundFrontmostNotFocused.HasValue)
                {
				    return ColorBackgroundFrontmostNotFocused.Value;
                }
                else
                {
                    return null;
                }
			}
		}
		else
		{
            if (ColorBackgroundNotFrontmost.HasValue)
            {
			    return ColorBackgroundNotFrontmost.Value;
            }
            else
            {
                return null;
            }
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
	public virtual void OnFrontmost() { }
	public virtual void OnNotFrontmost() { }

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

	public List<IFocusable> GetAllFocusables()
	{
		 List<IFocusable> toReturn = new List<IFocusable> ();
		if (this.CanFocus)
		{
			toReturn.Add(this as IFocusable);
		}

		foreach (var i in ChildrenCollection)
		{
			toReturn.AddRange((i as UIObject).GetAllFocusables());
		}

		return toReturn;
	}

	public virtual void OnFoucsed() { }
	public virtual void OnUnfocused() { }

	public virtual bool Navigate(NavigationDirection direction)
	{
		if (!FocusCollection.ContainsKey(direction))
		{
			return false;
		}

		UIManager.instance.SetCurrentFocusObject(FocusCollection[direction]);
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
		if (CurrentAnchorPoint.Horizontal != AnchorPointHorizonal.stretch && CurrentAnchorPoint.Vertical != AnchorPointVertical.stretch)
		{
			return base.GetScreenPosition();
		}
		Point newPoint = base.GetScreenPosition();
		if (CurrentAnchorPoint.Horizontal != AnchorPointHorizonal.stretch)
		{
			newPoint.X += OffsetLeft;
		}
		if (CurrentAnchorPoint.Vertical != AnchorPointVertical.stretch)
		{
			newPoint.Y += OffsetTop;
		}
		return newPoint;
	}

	public Rect GetScreenSpaceRect()
	{
		return GetRect();
	}

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

		switch (CurrentAnchorPoint.Horizontal)
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
		switch (CurrentAnchorPoint.Vertical)
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
		//Log.WriteLine(Name + " --> " + GetRect().ToString());
		//Log.WriteLine("Drawing: " + Name);
	}


}