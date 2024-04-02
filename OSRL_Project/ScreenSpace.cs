public class ScreenSpace
{
  public Point Location;
  public int X => Location.X;
  public int Y => Location.Y;

  public int Width, Height;

  public ScreenSpace(int x, int y, int width, int height)
  {
    Location = new Point (x,y);
    Width = width;
    Height = height;
  }

  public ScreenSpace(Point point, int width, int height)
  {
    Location = point;
    Width = width;
    Height = height;
  }
}