public class UI_GroupBase : UIObject
{
#region Constructors
public UI_GroupBase(string name, Point screenPosition, int width, int height)
		: base(name, screenPosition, width, height)
	{

	}
	public UI_GroupBase(string name, int x, int y, int width, int height)
		: base(name, x, y, width, height)
	{

	}
#endregion

    public int Spacing = 0;
    public int SpacingBefore = 0;
    public int SpacingAfter = 0;
    public bool UseGroupObjectHeight = false;
    public int GroupObjectHeight = 1;
    
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
		
	}
}