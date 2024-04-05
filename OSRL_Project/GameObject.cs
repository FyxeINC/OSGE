
using System.Diagnostics;

public class GameObject : IDrawable, ITickable
{
	public ulong ID;

	public virtual bool CanTick {get; set;} = false;

    
    public bool IsDirty {get; set;} = true;

    public void SetIsDirty(bool isDirty)
    {
        IsDirty = isDirty;
        if (IsDirty)
        {
            for (int i = ChildrenCollection.Count-1; i >= 0; i--)
            {
                ChildrenCollection[i].SetIsDirty(true);
            }
        }
    }

#region Constructors
	public GameObject()
	{
		Name = "GameObject";
		ScreenPosition = new Point(0,0);
		this.Register();
	}
	public GameObject(string name, int x, int y) 
	{
		Name = name;
		ScreenPosition = new Point (x,y);
		this.Register();
	}
	public GameObject(string name, Point screenPosition)
	{
		Name = name;
		ScreenPosition = screenPosition;
		this.Register();
	}

	~GameObject()
	{
		this.Unregister();
	}
#endregion

	public virtual ConsoleColor GetColorForeground()
	{
		return ConsoleColor.White;
	}

	public virtual ConsoleColor? GetColorBackground()
	{
		return null;
	}
		
	public Point ScreenPosition;
	public GameObject Parent { get; set; }
	protected List<GameObject> ChildrenCollection = new List<GameObject> ();

	public string Name = "GameObject";

	public virtual void SetParent(GameObject newParent)
	{
		Parent = newParent;
        SetIsDirty(true);
	}

	public void SetScreenPosition(Point point) { SetScreenPosition(point.X, point.Y); }
	public void SetScreenPosition(int x, int y)
	{
		ScreenPosition.X = x;
		ScreenPosition.Y = y;
        SetIsDirty(true);
	}

	public virtual Point GetScreenPosition()
	{
		if (Parent != null)
		{
			//Debug.WriteLine(Name + " parent not null");
			return ScreenPosition + Parent.GetScreenPosition();
		}
		else
		{
			//Debug.WriteLine(Name + " parent IS null");
			return ScreenPosition;
		}
	}

	public Point GetLocalPosition()
	{
		return ScreenPosition;
	}

	public virtual void Tick(float deltaTime) {}

	public virtual void Draw() 
	{
		for (int i = ChildrenCollection.Count-1; i >= 0; i--)
		{
			ChildrenCollection[i].Draw();
		}
		// foreach (var i in ChildrenCollection)
		// {
		//     i.Draw();
		// }
        IsDirty = false;
	}

	public override string ToString()
	{
		return $"{ID}:{Name}";
	}

	public List<GameObject> GetChildren()
	{
		return ChildrenCollection;
	}

	public int GetChildrenCount()
	{
		return ChildrenCollection.Count;
	}

	public bool ContainsChild(GameObject child)
	{
		return ChildrenCollection.Contains(child);
	}

	public virtual void AddChild(GameObject newChild, bool frontmost = true)
	{
		if (newChild == null)
		{
			return;
		}

		if (Parent == newChild)
		{
			return;
		}

		if (ChildrenCollection.Contains(newChild))
		{
			return;
		}
		if (frontmost)
		{
			ChildrenCollection.Insert(0, newChild);
		}
		else
		{
			ChildrenCollection.Add(newChild);
		}
		newChild.SetParent(this);
	}

	public virtual void RemoveChild(GameObject toRemove)
	{
		if (!ChildrenCollection.Contains(toRemove))
		{
			return;
		}
		if (toRemove.Parent != this)
		{
			return;
		}

		ChildrenCollection.Remove(toRemove);
		toRemove.SetParent(null);
	}

	public virtual void SetChildIndex(GameObject child, int index)
	{
		if (child == null)
		{
			return;
		}
		if (!ChildrenCollection.Contains(child) || ChildrenCollection.IndexOf(child) == index)
		{
			return;
		}

		ChildrenCollection.Remove(child);
		ChildrenCollection.Insert(index, child);
	}

	public int GetChildIndex(GameObject child)
	{
		if (child == null)
		{
			return -1;
		}
		return ChildrenCollection.IndexOf(child);
	}
}