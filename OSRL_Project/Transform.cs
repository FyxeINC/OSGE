public enum AnchorType
{
  /*
    x  x  x | [===]
    x  x  x | [===]
    x  x  x | [===]
    --------  
    ^  ^  ^   X
    |  |  |
    v  v  v
  */
  topLeft,
  // topCenter,
  // topRight,
  // centerLeft,
  // center,
  // centerRight,
  // bottomLeft,
  // bottomCenter,
  // bottomRight,
  topCenterWidthStretch,
  // centerWidthStretch,
  // bottomCenterWidthStretch,
  // leftHeightStretch,
  // centerHeightStretch,
  // rightHeightStretch,
  stretch
}

public class Transform
{
  protected Transform m_Parent = null;

  protected AnchorType m_AnchorType = AnchorType.topLeft;

  public Point StartPoint;
  public int X {get {return StartPoint.X;} set {StartPoint.X = value;}}
  public int Y {get {return StartPoint.Y;} set {StartPoint.Y = value;}}
  public int Width, Height;

  public TransformOffset Offset;

  public Rect GetRect()
  {
    Rect toReturn = new Rect ();

    Rect parentRect = new Rect ();
    if (m_Parent != null)
    {
      parentRect = m_Parent.GetRect();
    }

    switch (m_AnchorType)
    {
      case AnchorType.topLeft:
        toReturn.Location.X = X + parentRect.Location.X;
        toReturn.Location.Y = Y + parentRect.Location.Y;

        toReturn.Width = Width;
        toReturn.Height = Height;
        break;
        
      case AnchorType.topCenterWidthStretch:        
        toReturn.Location.X = parentRect.Location.X + Offset.Left;
        toReturn.Location.Y = Y + parentRect.Location.Y;

        toReturn.Width = parentRect.Width - Offset.Left - Offset.Right;
        toReturn.Height = Height;
        break;
      case AnchorType.stretch:
        toReturn.Location.X = parentRect.Location.X + Offset.Left;
        toReturn.Location.Y = parentRect.Location.Y + Offset.Top;

        toReturn.Width = parentRect.Width - Offset.Left - Offset.Right;
        toReturn.Height = parentRect.Height - Offset.Top - Offset.Bottom;
        break;
      default:
        break;
    }
    return toReturn;
  }

  public Transform(AnchorType anchorType, Point startPoint, int width, int height, TransformOffset offset, Transform parent = null)
  {
    m_AnchorType = anchorType;
    StartPoint = startPoint;
    Width = width;
    Height = height;
    Offset = offset;
    m_Parent = parent;
  }
}