public class UI_Panel : UIObject
{
    #region Constructors
    public UI_Panel(string name, int x, int y, int width, int height, 
      ConsoleColor foreground, 
      ConsoleColor background,
      ConsoleColor foregroundNotFrontmost, 
      ConsoleColor backgroundNotFrontmost, 
      BorderType borderType, 
      string panelTitle, 
      char panelBackgroundChar = ' ') 
        : base(name, x, y, width, height)
    {
      UISolidFill = new UI_SolidFill (name + "_Fill", 0, 0, 0, 0, foreground, background, foregroundNotFrontmost, backgroundNotFrontmost, panelBackgroundChar);
      UISolidFill.UseOffsetLeftRight = true;
      UISolidFill.UseOffsetTopBottom = true;
      UIBorder = new UI_Border (borderType, foreground, background, foregroundNotFrontmost, backgroundNotFrontmost);
      UIBorder.Name = name + "_Border";
      UITextArea = new UI_TextArea (name + "_Text", 0, 0, 0, 1, foreground, background, foregroundNotFrontmost, backgroundNotFrontmost, panelTitle);
      UITextArea.UseOffsetLeftRight = true;
      UITextArea.OffsetLeft = 3;
      UITextArea.OffsetRight = 3;

      this.AddChild(UITextArea);
      this.AddChild(UIBorder);
      this.AddChild(UISolidFill);
    }
    #endregion

    UI_TextArea UITextArea;
    UI_Border UIBorder;
    UI_SolidFill UISolidFill;
}