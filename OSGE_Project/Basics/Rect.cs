public class Rect
{
  public Point Location;
  public int X => Location.X;
  public int Y => Location.Y;

  public int Width, Height;

  public Rect()
  {
    Location = new Point (0,0);
    Width = 0;
    Height = 0;
  }

  public Rect(int x, int y, int width, int height)
  {
    Location = new Point (x,y);
    Width = width;
    Height = height;
  }

  public Rect (Point point, int width, int height)
  {
    Location = point;
    Width = width;
    Height = height;
  }

  public bool Contains(int x, int y)
  {
    // TODO - check
    if (x >= X && x < X + Width && 
        y >= Y && y < Y + Height)
    {
        return true;
    }

    return false;
  }

  public override string ToString()
  {
      return $"(X:{X} | Y:{Y}  | W:{Width} | H:{Height})";
  }

  public int GetArea()
  {
    return Width * Height;
  }
}