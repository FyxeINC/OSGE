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
      UISolidFill.SetAnchorPoint(AnchorPointHorizonal.stretch, AnchorPointVertical.stretch);
      UIBorder = new UI_Border (borderType, foreground, background, foregroundNotFrontmost, backgroundNotFrontmost);
      UIBorder.SetAnchorPoint(AnchorPointHorizonal.stretch, AnchorPointVertical.stretch);
      UIBorder.Name = name + "_Border";
      UITextArea = new UI_TextArea (name + "_Text", 0, 0, 0, 1, foreground, background, foregroundNotFrontmost, backgroundNotFrontmost, panelTitle);
      UITextArea.SetAnchorPoint(AnchorPointHorizonal.stretch, AnchorPointVertical.top);
      UITextArea.SetOffset(0,0,3,3);
      UITextArea.SetScreenPosition(0, 0);
      UITextArea.SetSize(10, 1);

      this.AddChild(UITextArea, false);
      this.AddChild(UIBorder, false);
      this.AddChild(UISolidFill, false);
    }
    #endregion

    UI_TextArea UITextArea;
    UI_Border UIBorder;
    UI_SolidFill UISolidFill;
}