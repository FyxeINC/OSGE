using ConsoleRenderer;
using ConsoleHelperLibrary.Classes;


public class Display
{
    protected ConsoleCanvas m_DisplayCanvas;

    public Display()
    {        
		  m_DisplayCanvas = new ConsoleCanvas(true, true).CreateBorder();
    }

    public void Render()
    {
      for (int y = 0; y < m_DisplayCanvas.Height; y++)
            {
                for (int x = 0; x < m_DisplayCanvas.Width; x++)
                {
                    ConsoleColor color = Random.Shared.NextSingle() < 0.5 ? ConsoleColor.White : ConsoleColor.Black;
                    m_DisplayCanvas.Set(x, y, ' ', color, color);
                }
            }
      m_DisplayCanvas.Render();
    }

}