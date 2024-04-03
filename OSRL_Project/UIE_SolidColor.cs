public class UIE_SolidColor : UIElement
{
    public char DisplayChar = ' ';
    public ConsoleColor Foreground = ConsoleColor.White;
    public ConsoleColor Background = ConsoleColor.Black;

    public override void Render()
    {
        base.Render();
        Rect rect = m_Transform.GetRect();
        for (int x = rect.Location.X; x < rect.Width; x++)
        {
            for (int y = rect.Location.Y; y < rect.Height; y++)
            {
                CanvasDrawer.Set(x, y, DisplayChar, Foreground, Background);
            }
        }
    }
}