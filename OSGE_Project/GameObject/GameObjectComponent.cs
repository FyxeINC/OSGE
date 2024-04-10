public class GameObjectComponent : ITickable
{
    protected GameObject m_Parent;

    bool m_CanTick = false;

    public virtual bool CanTick()
    {
        return m_CanTick;
    }

    public virtual void SetCanTick(bool newCanTick)
    {
        m_CanTick = newCanTick;
    }

    public virtual T GetParent<T>() where T : GameObject
    {
        return m_Parent as T;
    }

    public void SetParent(GameObject newParent)
    {
        m_Parent = newParent;
    }

    public T GetComponent<T>() where T : GameObjectComponent
    {
        if (m_Parent == null)
        {
            return null;
        }

        foreach(var i in m_Parent.GetComponentCollection())
        {
            if (i is T)
            {
                return i as T;
            }
        }
        return null;
    }
    
    public virtual void Awake()
    {

    }

    public virtual void Start()
    {
        
    }

    public virtual void Tick(long deltaTime)
    {
        
    }

    public override string ToString()
    {
        if (m_Parent != null)
        {
            return m_Parent.Name + "_" + this.GetType().ToString();
        }
        else
        {
            return "NULL_" + this.GetType().ToString();
        }
    }
}