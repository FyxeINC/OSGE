
using System.Diagnostics;

public class GameObject : IDrawable, ITickable
{

#region ITickable

    bool m_CanTick = false;
    /// <summary>
    /// DO NOT set to true, call SetCanTick
    /// </summary>
	public virtual bool CanTick()
    {
        return m_CanTick;
    }

    /// <summary>
    /// Change if this GameObject recieves Tick() updates. Note: false by default, must be enabled post construction.
    /// </summary>
    public void SetCanTick(bool canTick)
    {
        if (canTick == m_CanTick)
        {
            return;
        }

        m_CanTick = canTick;

        if (m_CanTick)
        {
            TimeManager.instance.Register(this);
        }
        else
        {
            TimeManager.instance.Unregister(this);
        }
    }
#endregion

#region IDrawable    
    public bool IsDirty {get; set;} = true;
    
	public virtual ConsoleColor GetColorForeground()
	{
		return ConsoleColor.White;
	}

	public virtual ConsoleColor? GetColorBackground()
	{
		return null;
	}

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

    /// <summary>
    /// Draws children and sets IsDirty to false. Call this function AFTER you do your draw logic to ensure your child objects are in front.
    /// </summary>
	public virtual void Draw() 
	{
		//for (int i = GetChildrenCollectionCount()-1; i >= 0; i--)
        for (int i = 0; i < GetChildrenCollectionCount(); i++)
		{
			ChildrenCollection[i].Draw();
		}
		// foreach (var i in ChildrenCollection)
		// {
		//     i.Draw();
		// }
        IsDirty = false;
	}
#endregion

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
		
	
    /// <summary>
    /// The unique ID assigned in the GameObject's construction.
    /// </summary>
	public ulong ID;
    public Point ScreenPosition;
    /// <summary>
    /// The GameObject that this object is parented to. Can be null. This object's position will be relative to its parent's position.
    /// </summary>
	public GameObject Parent { get; set; }
	public string Name {get; set;} = "GameObject";
	protected List<GameObject> ChildrenCollection = new List<GameObject> ();    

    List<GameObjectComponent> ComponentCollection = new List<GameObjectComponent> ();

    public List<GameObjectComponent> GetComponentCollection()
    {
        return ComponentCollection;
    }
    
	public virtual void SetParent(GameObject newParent)
	{
		Parent = newParent;
        SetIsDirty(true);
	}

    public virtual T AddComponent<T>() where T : GameObjectComponent, new()
    {
        T newComponent = new T ();
        newComponent.SetParent(this);
        ComponentCollection.Add(newComponent);
        newComponent.Awake();
        return newComponent;
    }

    public virtual void RemoveComponent(GameObjectComponent toRemove)
    {
        ComponentCollection.Remove(toRemove);
    }

	public void SetLocalPosition(Point point) { SetLocalPosition(point.X, point.Y); }
	public void SetLocalPosition(int x, int y)
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

	public virtual List<GameObject> GetChildrenCollection()
	{
		return ChildrenCollection;
	}

	public int GetChildrenCollectionCount()
	{
		return ChildrenCollection.Count;
	}

	public bool ContainsChild(GameObject child)
	{
		return ChildrenCollection.Contains(child);
	}

	public virtual void AddChild(GameObject newChild, bool sendToBackground = false)
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
		if (sendToBackground)
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
    
	public override string ToString()
	{
		return $"{ID}:{Name}";
	}
    
    /// <summary>
    /// Called after GameObject is registered and has ID
    /// </summary>
    public virtual void Awake()
    {
        foreach (var i in ComponentCollection)
        {
            i.Awake();
        }

        // TODO - associate with level or something
        Start();
    }

    /// <summary>
    /// Called after Awake()
    /// </summary>
    public virtual void Start()
    {
        foreach (var i in ComponentCollection)
        {
            i.Start();
        }
    }

    public virtual void Tick(long deltaTime)
    {
        foreach (var i in ComponentCollection)
        {
            if (i.CanTick())
            {
                i.Tick(deltaTime);
            }
        }
    }
}