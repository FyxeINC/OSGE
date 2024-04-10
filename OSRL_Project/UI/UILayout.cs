/// <summary>
/// A unique UI object that creates "blank" children to act as layers. 
/// </summary>
public class UILayout : UIObject
{
    public UILayout(string name, Point screenPosition, int width, int height)
		: base(name, screenPosition, width, height)
	{
		Width = width;
		Height = height;
	}

	public UILayout(string name, int x, int y, int width, int height)
		: base(name, x, y, width, height)
	{
		Width = width;
		Height = height;
	}


    public Dictionary<Tag, UIObject> UILayerCollection = new Dictionary<Tag, UIObject> ();
    int m_FrontmostIndex = -1;

    public void CreateLayer(Tag layerTag)
    {
        if (UILayerCollection.ContainsKey(layerTag))
        {
            return;
        }

        UIObject newObject = new UIObject ("LayoutLayer_" + layerTag.ToString(), 0,0,0,0);
        newObject.SetAnchorPoint(AnchorPointHorizonal.stretch, AnchorPointVertical.stretch);
        newObject.SetOffset(0,0,0,0);

        UILayerCollection.Add(layerTag, newObject);
        AddChild(newObject);
    }

    public void AddUIObject(UIObject uIObject, Tag layerTag, bool setToFront = false)
    {
        if (!UILayerCollection.ContainsKey(layerTag))
        {
            Log.Warning($"Tried to add UI to layer({layerTag}), which doesn't exist.");
            return;
        }

        UILayerCollection[layerTag].AddChild(uIObject, setToFront);
        
        RecalculateFocus();
    }

    public bool RemoveUIObject(UIObject objectToRemove)
    {
        bool didRemove = false;
        foreach (UIObject i in UILayerCollection.Values)
        {
            if (i.ContainsChild(objectToRemove))
            {
                i.RemoveChild(objectToRemove);
                didRemove = true;
                break;
            }
        }

        if (didRemove)
        {
            RecalculateFocus();
        }
        return didRemove;
    }

    // TODO - hook up to CanFocus
    public void RecalculateFocus()
    {   
        int previousFrontmostIndex = m_FrontmostIndex;

        // Set frontmost index to first child with focusable object
        // m_FrontmostIndex = -1;
        // for(int i = GetChildrenCollectionCount() -1; i >= 0; i--)
        // {
        //     if ((GetChildrenCollection()[i] as UIObject).CanAnyFocus())
        //     {
        //         m_FrontmostIndex = i;
        //         break;
        //     }
        // }

        // Set frontmost index to first child with children
        for(int i = GetChildrenCollectionCount() -1; i >= 0; i--)
        {
            if (GetChildrenCollection()[i].GetChildrenCollectionCount() >= 0)
            {
                m_FrontmostIndex = i;
                break;
            }
        }

        /*
        // This isn't a time save because we still need to recalc focus
        if (m_FrontmostIndex == previousFrontmostIndex)
        {
            // Frontmost has not changed, only need to recalc frontmost
            return;
        }
        */

        for(int i = GetChildrenCollectionCount() -1; i >= 0; i--)
        {
            (GetChildrenCollection()[i] as UIObject).SetIsFrontmost(i == m_FrontmostIndex);
        }

        if (m_FrontmostIndex != -1)
        {
            UIManager.instance.RecalculateFocusables((GetChildrenCollection()[m_FrontmostIndex] as UIObject));
        }
        else
        {
            UIManager.instance.RecalculateFocusables(null);
        }        
    }

    public override bool HandleBackAction()
    {
        if (m_FrontmostIndex == -1)
        {
            return false;
        }

        UIObject frontmostLayer = (GetChildrenCollection()[m_FrontmostIndex] as UIObject);
        // for(int i = frontmostLayer.GetChildrenCollectionCount(); i >= 0; i--)
        // {
        //     if ((frontmostLayer.GetChildrenCollection()[i] as UIObject).HandleBackAction())
        //     {
        //         return true;
        //     }
        // }
        if (frontmostLayer.GetChildrenCollectionCount() <= 0)
        {
            return false;
        }
        return (frontmostLayer.GetChildrenCollection()[frontmostLayer.GetChildrenCollectionCount()-1] as UIObject).HandleBackAction();
    }
}