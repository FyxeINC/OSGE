
using System.Runtime.CompilerServices;
using System.Threading.Channels;

public class UI_HorizontalGroup : UIObject
{
#region Constructors
	public UI_HorizontalGroup(string name, Point screenPosition, int width, int height) 
	: base(name, screenPosition, width, height)
	{
	}
#endregion
	
	public AnchorPointHorizonal ListAnchorPoint = AnchorPointHorizonal.left;
	public int Spacing = 0;
	public int ListObjectWidth = 1;

	public override void AddChild(GameObject newChild, bool frontmost = true)
	{
		base.AddChild(newChild, frontmost);
		UpdateChildPositions();
	}

	public override void RemoveChild(GameObject toRemove)
	{
		base.RemoveChild(toRemove);
		UpdateChildPositions();
	}

	public override void SetChildIndex(GameObject child, int index)
	{
		base.SetChildIndex(child, index);
		UpdateChildPositions();
	}

	protected virtual void UpdateChildPositions()
	{
		if (ListAnchorPoint == AnchorPointHorizonal.left)
		{
			int startingWidth = 0;
			for (int i = 0; i < GetChildrenCollectionCount(); i++)
			{
				UIObject childObject = (GetChildrenCollection()[i] as UIObject);
				childObject.SetAnchorPoint(AnchorPointHorizonal.left, AnchorPointVertical.stretch);
				childObject.SetSize(ListObjectWidth, 0);
				childObject.SetLocalPosition(startingWidth, 0);
				startingWidth += ListObjectWidth + Spacing;
			}
		}
		// todo
	}
}