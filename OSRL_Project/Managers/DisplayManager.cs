using ConsoleRenderer;
using ConsoleHelperLibrary.Classes;
using System.ComponentModel;


public static class DisplayManager
{
    private static ConsoleCanvas m_DisplayCanvas = null;
    public static bool IsDirty = true;

    /// <summary>
    /// The frames per second to attempt to tick all objects by.
    /// </summary>
    public static int FPSTarget = 120;
    public static long TimeSinceLastFrame = 0;

    public static void Initialize()
    {        
        // Window Height is given -1 to prevent the single scroll that occurs. 
        // TODO - investigate, there may be a way to prevent scrolling
        m_DisplayCanvas = new ConsoleCanvas(Console.WindowWidth, Console.WindowHeight-1, false, false);
    }

    /// <summary>
    /// Sets a pixel within the display. If a background is not provided, it will use the color previously there.
    /// </summary>
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
        
        Pixel pixel = m_DisplayCanvas.Get(x,y, true);

        ConsoleColor backgroundToUse = pixel.Background;
        if (background.HasValue)
        {
            backgroundToUse = background.Value;
        }

        Pixel previous = m_DisplayCanvas.Get(x, y, false);
        if (previous.Character != character || previous.Foreground != foreground || previous.Background != backgroundToUse)
        {
            m_DisplayCanvas.Set(x, y, character, foreground, backgroundToUse);
            IsDirty = true;
        }
    }

    // public static void DrawText(int x, int y, string text, bool centered = false, ConsoleColor? foreground = null, ConsoleColor? background = null)
    // {
    //     if (m_DisplayCanvas == null)
    //     {
    //         return;
    //     }
    //     // TODO - make text respect previously set color
    //     m_DisplayCanvas.Text(x, y, text, centered, foreground, background);
    // }

    /// <summary>
    /// Renders the current screen if dirty.
    /// </summary>
    public static void Render(bool force = false)
    {
        if (m_DisplayCanvas == null )
        {
            return;
        }

        if (!IsDirty && !force)
        {
            return;
        }
        m_DisplayCanvas.Render();
        IsDirty = false;
    }

    public static void Tick(long deltaTime)
    {
        TimeSinceLastFrame += deltaTime;

        if (TimeSinceLastFrame > (1 / FPSTarget) * 1000)
        {
            TimeSinceLastFrame = 0;
            // TODO - I dislike this, but I don't have a way to force Tick positining yet
            UIManager.Draw();
            Render();
        }
    }
}