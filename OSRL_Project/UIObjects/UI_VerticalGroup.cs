
using System.Threading.Channels;

public class UI_VerticalGroup : UIObject
{
#region Constructors
    public UI_VerticalGroup(string name, Point screenPosition, int width, int height, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.DarkGray, ConsoleColor foregroundNotFrontmost = ConsoleColor.Gray, ConsoleColor backgroundNotFrontmost = ConsoleColor.Black) : base(name, screenPosition, width, height, foreground, background, foregroundNotFrontmost, backgroundNotFrontmost)
    {
    }
#endregion
    
    public AnchorPointVertical ListAnchorPoint = AnchorPointVertical.top;
    public int Spacing = 0;
    public int ListObjectHeight = 1;

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
        if (ListAnchorPoint == AnchorPointVertical.top)
        {
            int startingHeight = 0;
            for (int i = 0; i < GetChildrenCount(); i++)
            {
                UIObject childObject = (GetChildren()[i] as UIObject);
                childObject.SetAnchorPoint(AnchorPointHorizonal.stretch, AnchorPointVertical.top);
                childObject.SetSize(0, ListObjectHeight);
                childObject.SetScreenPosition(0, startingHeight);
                startingHeight += ListObjectHeight + Spacing;
            }
        }
        else if (ListAnchorPoint == AnchorPointVertical.bottom)
        {
            int startingHeight = 0;
            for (int i = GetChildrenCount()-1; i >= 0; i--)
            {
                UIObject childObject = (GetChildren()[i] as UIObject);
                childObject.SetAnchorPoint(AnchorPointHorizonal.stretch, AnchorPointVertical.bottom);
                childObject.SetSize(0, ListObjectHeight);
                childObject.SetScreenPosition(0, startingHeight);
                startingHeight -= ListObjectHeight + Spacing;
            }
        }
    }
}