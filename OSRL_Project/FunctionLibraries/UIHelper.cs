public static class UIHelper
{
	public static AnchorPoint AnchorTopLeft = new AnchorPoint (AnchorPointHorizonal.left, AnchorPointVertical.top);
	public static AnchorPoint AnchorTopCenter = new AnchorPoint (AnchorPointHorizonal.center, AnchorPointVertical.top);
	public static AnchorPoint AnchorTopRight = new AnchorPoint (AnchorPointHorizonal.right, AnchorPointVertical.top);
	public static AnchorPoint AnchorMiddleLeft = new AnchorPoint (AnchorPointHorizonal.left, AnchorPointVertical.middle);
	public static AnchorPoint AnchorMiddleCenter = new AnchorPoint (AnchorPointHorizonal.left, AnchorPointVertical.middle);
	public static AnchorPoint AnchorMiddleRight = new AnchorPoint (AnchorPointHorizonal.center, AnchorPointVertical.middle);
	public static AnchorPoint AnchorBottomLeft = new AnchorPoint (AnchorPointHorizonal.left, AnchorPointVertical.bottom);
	public static AnchorPoint AnchorBottomCenter = new AnchorPoint (AnchorPointHorizonal.center, AnchorPointVertical.bottom);
	public static AnchorPoint AnchorBottomRight = new AnchorPoint (AnchorPointHorizonal.right, AnchorPointVertical.bottom);
	public static AnchorPoint AnchorStretch = new AnchorPoint (AnchorPointHorizonal.stretch, AnchorPointVertical.stretch);
	
	public static void CreateVerticalFocusMappings(List<UIObject> toMap, bool allowLoop = true)
	{
		if (toMap == null)
		{
			return;
		}
		if (toMap.Count <= 1)
		{
			return;
		}

		for (int i = 0; i < toMap.Count; i++)
		{
			if (i == 0)
			{
				if (allowLoop)
				{
					toMap[i].SetFocusRelation(toMap[toMap.Count-1], NavigationDirection.up);
				}
			}
			else
			{
				toMap[i].SetFocusRelation(toMap[i-1], NavigationDirection.up);
			}

		if (i == toMap.Count-1)
		{
			if (allowLoop)
			{
				toMap[i].SetFocusRelation(toMap[0], NavigationDirection.down);  
			}
			}
			else
			{
				toMap[i].SetFocusRelation(toMap[i+1], NavigationDirection.down);      
			}
		}
	}
    public static void CreateHorizontalFocusMappings(List<UIObject> toMap, bool allowLoop = true)
	{
		if (toMap == null)
		{
			return;
		}
		if (toMap.Count <= 1)
		{
			return;
		}

		for (int i = 0; i < toMap.Count; i++)
		{
			if (i == 0)
			{
				if (allowLoop)
				{
					toMap[i].SetFocusRelation(toMap[toMap.Count-1], NavigationDirection.right);
				}
			}
			else
			{
				toMap[i].SetFocusRelation(toMap[i-1], NavigationDirection.right);
			}

		if (i == toMap.Count-1)
		{
			if (allowLoop)
			{
				toMap[i].SetFocusRelation(toMap[0], NavigationDirection.left);  
			}
			}
			else
			{
				toMap[i].SetFocusRelation(toMap[i+1], NavigationDirection.left);      
			}
		}
	}

    public static void DestroyUIObject(this UIObject uiObject)
    {
        if (UIManager.instance.Layout == null)
        {
            Log.Error("Cannot Destroy UI Object when Layout is Null");
            return;
        }

        UIManager.instance.Layout.RemoveChild(uiObject);          
        UIManager.instance.UpdateCurrentFrontmostObject();              
    }
}