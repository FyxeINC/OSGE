

using System.Dynamic;

public class UIManager
{
    public List<UIObject> UIObjectCollection = new List<UIObject> ();
    protected UIObject CurrentFocusObject = null;

    public UIObject GetCurrentFocusObject() 
    { 
        if (UIObjectCollection.Count <= 0)
        {
            return null;
        }
        else 
        {
            return UIObjectCollection[0];
        }
    }

    public bool SetCurrentFocusObject(UIObject newObject)
    {
        int index = UIObjectCollection.IndexOf(newObject);
        if (index == -1)
        {
            return false;
        }
        else if (index == 0)
        {
            return false;
        }
        UIObject currentFocus = GetCurrentFocusObject();
        if (currentFocus != null)
        {
            GetCurrentFocusObject().OnUnfocused();
        }

        UIObjectCollection.Remove(newObject);
        UIObjectCollection.Insert(0, newObject);

        GetCurrentFocusObject().OnFocused();
        
        return true;
    }
}