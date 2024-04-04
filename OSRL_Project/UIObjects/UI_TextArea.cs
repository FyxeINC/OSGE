using System.Data;

public enum TextAlignmentHorizontal
{
    left, center, right
}
public enum TextAlignmentVertical
{
    top, middle, bottom
}

public class UI_TextArea : UIObject
{
    #region Constructors

    public UI_TextArea(string name, int x, int y, int width, int height,
        ConsoleColor foreground, 
        ConsoleColor background, 
        ConsoleColor foregroundNotFrontmost, 
        ConsoleColor backgroundNotFrontmost, 
        string text) 
        : base(name, x, y, width, height, foreground, background, foregroundNotFrontmost, backgroundNotFrontmost)
    {
        Text = text;
    }
    #endregion
    string Text;
    TextAlignmentHorizontal AlignmentHorizontal = TextAlignmentHorizontal.left;
    TextAlignmentVertical AlignmentVertical = TextAlignmentVertical.top;

    public override void Draw()
    {
        Rect rect = GetRect();
        int stringIndex = 0;
        string toUse = Text;
        if (IsFocused)
        {
            toUse = ">" + Text;
        }
        
        for (int x = 0; x < rect.Width; x++)
        {
            for (int y = 0; y < rect.Height; y++)
            {
                if (stringIndex < toUse.Length)
                {
                    DisplayManager.Draw(x + rect.X, y + rect.Y, toUse[stringIndex], GetColorForeground(), GetColorBackground());
                    stringIndex += 1;
                }
            }
        }
        base.Draw();
    }
}