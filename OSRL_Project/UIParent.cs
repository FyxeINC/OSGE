

public class UIParent : IRenderable
{
    protected ScreenSpace m_ScreenSpace;

    public UIParent(ScreenSpace screenSpace)
    {
        m_ScreenSpace = screenSpace;
    }

    List<UIElement> UIElementCollection = new List<UIElement> ();

    protected bool m_IsFocused = false;
    public bool GetIsFocused() {return m_IsFocused;}

    public bool SetIsFocused(bool newIsFocused)
    {
        if (newIsFocused == m_IsFocused)
        {
            return false;
        }

        m_IsFocused = newIsFocused;

        if (m_IsFocused)
        {
            OnFocused();
        }
        else
        {
            OnUnfocused();
        }

        return true;
    }

    public virtual void OnFocused() 
    {
        m_IsFocused = true;
        foreach (var i in UIElementCollection)
        {
            i.OnFocused();
        }
    }

    public virtual void OnUnfocused() 
    {
        m_IsFocused = false;
        foreach (var i in UIElementCollection)
        {
            i.OnUnfocused();
        }
    }

    public virtual void Destroy()
    {
        UIManager.DestroyUIObject(this);
    }

    public override void Render()
    {
        base.Render();
        foreach (var i in UIElementCollection)
        {
          i.Render();
        }
    }
}