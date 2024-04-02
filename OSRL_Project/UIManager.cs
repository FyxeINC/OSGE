

using System.Dynamic;

public static class UIManager
{
    public static List<UIParent> UIParentCollection = new List<UIParent> ();
    static UIParent CurrentFocusObject = null;

    public static UIParent GetCurrentFocusObject() 
    { 
        if (UIParentCollection.Count <= 0)
        {
            return null;
        }
        else 
        {
            return UIParentCollection[0];
        }
    }

    public static bool SetCurrentFocusObject(UIParent uiObject)
    {
        int index = UIParentCollection.IndexOf(uiObject);
        if (index == -1)
        {
            return false;
        }
        else if (index == 0)
        {
            return false;
        }

        UIParentCollection.Remove(uiObject);
        UIParentCollection.Insert(0, uiObject);

        UpdateCurrentFocusObject();
        
        return true;
    }

    public static void UpdateCurrentFocusObject()
    {
        for (int i = 0; i < UIParentCollection.Count; i++)
        {
            UIParentCollection[i].SetIsFocused(i == 0);
        }
    }

    public static T CreateUIObject<T>(bool shouldFocus = false) where T : UIParent, new()
    {        
        T newUIObject = new T();

        UIParentCollection.Add(newUIObject);

        if (shouldFocus)
        {
            // Calls update focus object
            SetCurrentFocusObject(newUIObject);
        }
        else
        {
            UpdateCurrentFocusObject();
        }

        return newUIObject;
    }

    public static void DestroyUIObject(this UIParent uiObject)
    {
        UIParentCollection.Remove(uiObject);          
        UpdateCurrentFocusObject();              
    }

    public static void RenderAll()
    {
        foreach (var i in UIParentCollection)
        {
            i.Render();
        }
    }
}