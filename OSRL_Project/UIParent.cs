

using System.Diagnostics;

public class UIParent : IDrawable
{

    Dictionary<string, UIElement> UIElementCollection = new Dictionary<string, UIElement> ();

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
            i.Value.OnFocused();
        }
    }

    public virtual void OnUnfocused() 
    {
        m_IsFocused = false;
        foreach (var i in UIElementCollection)
        {
            i.Value.OnUnfocused();
        }
    }

    public virtual void Destroy()
    {
        UIManager.DestroyUIObject(this);
    }

    public override void Draw()
    {
        base.Draw();
        foreach (var i in UIElementCollection)
        {
          i.Value.Draw();
        }
    }

    public T AddUIElement<T>(string elementKey) where T : UIElement, new()
    {
        if (UIElementCollection.ContainsKey(elementKey))
        {
            Debug.WriteLine($"Element Key already exists for string({elementKey})");
            return null;
        }

        T newElement = new T ();
        UIElementCollection.Add(elementKey, newElement);
        return newElement;
    }

    public void RemoveUIElement(string elementKey)
    {
        UIElementCollection.Remove(elementKey);
    }
}