using ConsoleRenderer;
using ConsoleHelperLibrary.Classes;


public static class DisplayManager
{
    private static ConsoleCanvas m_DisplayCanvas = null;

    public static void Initialize()
    {        
        m_DisplayCanvas = new ConsoleCanvas(false, false);
    }

    public static void Draw(int x, int y, char character, ConsoleColor foreground, ConsoleColor? background = null)
    {
        if (x < 0 || x >= m_DisplayCanvas.Width)
        {
            return;
        }
        else if (y < 0 || y >= m_DisplayCanvas.Height)
        {
            return;
        }

        if (m_DisplayCanvas == null)
        {
            return;
        }
        if (background.HasValue)
        {
            m_DisplayCanvas.Set(x, y, character, foreground, background.Value);
        }
        else
        {
            Pixel pixel = m_DisplayCanvas.Get(x,y, false);
            m_DisplayCanvas.Set(x, y, character, foreground, pixel.Background);
        }
    }

    public static void DrawText(int x, int y, string text, bool centered = false, ConsoleColor? foreground = null, ConsoleColor? background = null)
    {
        if (m_DisplayCanvas == null)
        {
            return;
        }
        // TODO - make text respect previously set color
        m_DisplayCanvas.Text(x, y, text, centered, foreground, background);
    }

    public static void Render()
    {
        if (m_DisplayCanvas == null)
        {
            return;
        }
        m_DisplayCanvas.Render();
    }

}