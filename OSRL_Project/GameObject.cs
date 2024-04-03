
public class GameObject : IDrawable
{
    // TODO - object ID?
    #region Constructors
    public GameObject()
    {
        Name = "GameObject";
        Position = new Point(0,0);
    }
    public GameObject(string name, int x, int y) 
    {
        Name = name;
        Position = new Point (x,y);
    }
    public GameObject(string name, Point position)
    {
        Name = name;
        Position = position;
    }
    #endregion

    public Point Position;
    public GameObject Parent { get; set; }
    protected List<GameObject> ChildrenCollection = new List<GameObject> ();

    public string Name = "GameObject";

    public void SetParent(GameObject newParent)
    {
        Parent = newParent;
    }

    public void SetPosition(Point point) { SetPosition(point.X, point.Y); }
    public void SetPosition(int x, int y)
    {
        Position.X = x;
        Position.Y = y;
    }

    public Point GetPosition()
    {
        if (Parent != null)
        {
            return Position + Parent.GetPosition();
        }
        else
        {
            return Position;
        }
    }

    public Point GetLocalPosition()
    {
        return Position;
    }

    public override void Draw() {}

    public override string ToString()
    {
        return $"{Name} : ({GetPosition().X}) | ({GetPosition().Y})";
    }

    public List<GameObject> GetChildren()
    {
        return ChildrenCollection;
    }

    public int GetChildrenCount()
    {
        return ChildrenCollection.Count;
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
}