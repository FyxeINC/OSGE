public interface IDrawable
{
  public bool IsDirty {get; set;}
  public void Draw();
  public void AttemptDraw(bool force = false)
  {
    if (!IsDirty && !force)
    {
        return;
    }
    Draw();
  }

  public ConsoleColor GetColorForeground();
  public ConsoleColor? GetColorBackground();
}