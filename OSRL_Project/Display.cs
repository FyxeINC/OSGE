using ConsoleRenderer;
using ConsoleHelperLibrary.Classes;


public static class Display
{
    private static ConsoleCanvas m_DisplayCanvas = null;

    public static void Initialize()
    {        
        m_DisplayCanvas = new ConsoleCanvas(false, false);
    }

    public static void Draw(int x, int y, char character, ConsoleColor foreground, ConsoleColor background)
    {
        if (m_DisplayCanvas == null)
        {
            return;
        }
        m_DisplayCanvas.Set(x, y, character, foreground, background);
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