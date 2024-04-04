public static class UIHelper
{
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
}