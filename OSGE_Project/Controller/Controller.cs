public class Controller : ITickable
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

    Actor m_Actor;

    public virtual bool AttemptControlActor(Actor toControl)
    {
        m_Actor = toControl;
        m_Actor.SetController(this);
        OnControlActor(m_Actor);
        return true;
    }

    public virtual void OnControlActor(Actor controlled)
    {
        
    }
}