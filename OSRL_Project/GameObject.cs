
using System.Diagnostics;

public class GameObject : IDrawable
{
    // TODO - object ID?
    #region Constructors
    public GameObject()
    {
        Name = "GameObject";
        ScreenPosition = new Point(0,0);
    }
    public GameObject(string name, int x, int y) 
    {
        Name = name;
        ScreenPosition = new Point (x,y);
    }
    public GameObject(string name, Point screenPosition)
    {
        Name = name;
        ScreenPosition = screenPosition;
    }
    #endregion

    public virtual ConsoleColor GetColorForeground()
    {
        return ConsoleColor.White;
    }

    public virtual ConsoleColor GetColorBackground()
    {
        return ConsoleColor.Black;
    }


    public Point ScreenPosition;
    public GameObject Parent { get; set; }
    protected List<GameObject> ChildrenCollection = new List<GameObject> ();

    public string Name = "GameObject";

    public virtual void SetParent(GameObject newParent)
    {
        Parent = newParent;
    }

    public void SetScreenPosition(Point point) { SetScreenPosition(point.X, point.Y); }
    public void SetScreenPosition(int x, int y)
    {
        ScreenPosition.X = x;
        ScreenPosition.Y = y;
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
    }

    public override string ToString()
    {
        return $"{Name} : ({GetScreenPosition().X}) | ({GetScreenPosition().Y})";
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

    public void AddChild(GameObject newChild)
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

        ChildrenCollection.Add(newChild);
        newChild.SetParent(this);
    }

    public void RemoveChild(GameObject toRemove)
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

    public void SetChildIndex(GameObject child, int index)
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