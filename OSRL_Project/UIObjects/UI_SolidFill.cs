using System.Data;

public class UI_SolidFill : UIObject
{
    #region Constructors
    public UI_SolidFill(string name, int x, int y, int width, int height, 
        ConsoleColor foreground, 
        ConsoleColor background, 
        ConsoleColor foregroundNotFrontmost, 
        ConsoleColor backgroundNotFrontmost,
        char character = ' ') 
        : base(name, x, y, width, height, foreground, background, foregroundNotFrontmost, backgroundNotFrontmost)
    {
      Character = character;
    }
    #endregion

    char Character;

    public override void Draw()
    {
        Rect rect = GetRect();
        for (int x = 0; x < rect.Width; x++)
        {
            for (int y = 0; y < rect.Height; y++)
            {
                DisplayManager.Draw(x + rect.X, y + rect.Y, Character, GetColorForeground(), GetColorBackground());
            }
        }
        base.Draw();
    }
}