
public class UI_Panel : UIObject
{
#region Constructors
	public UI_Panel(string name, int x, int y, int width, int height,
		BorderType borderType, 
		string panelTitle, 
		char panelBackgroundChar = ' ') 
		: base(name, x, y, width, height)
	{
		UISolidFill = new UI_SolidFill (panelBackgroundChar);
        UISolidFill.Name = name + "_Fill";

		UIBorder = new UI_Border (BorderType.doubleLine);
		UIBorder.Name = name + "_Border";

		UITextArea = new UI_TextArea (name + "_Text", 0, 0, 0, 1, panelTitle);
		UITextArea.SetAnchorPoint(AnchorPointHorizonal.stretch, AnchorPointVertical.top);
		UITextArea.SetOffset(0,0,3,3);
		UITextArea.SetLocalPosition(0, 0);
		UITextArea.SetSize(10, 1);

		this.AddChild(UITextArea, false);
		this.AddChild(UIBorder, false);
		this.AddChild(UISolidFill, false);
	}
#endregion

	UI_TextArea UITextArea;
	UI_Border UIBorder;
	UI_SolidFill UISolidFill;

    public override void SetColors(
        ConsoleColor foregroundFrontmostFocused, 
        ConsoleColor foregroundFrontmostNotFocused, 
        ConsoleColor foregroundNotFrontmost, 
        ConsoleColor? backgroundFrontmostFocused, 
        ConsoleColor? backgroundFrontmostNotFocused, 
        ConsoleColor? backgroundNotFrontmost)
    {
        base.SetColors(
            foregroundFrontmostFocused, 
            foregroundFrontmostNotFocused, 
            foregroundNotFrontmost, 
            backgroundFrontmostFocused, 
            backgroundFrontmostNotFocused, 
            backgroundNotFrontmost);

        UISolidFill.SetColors(foregroundFrontmostFocused, foregroundFrontmostNotFocused, foregroundNotFrontmost, backgroundFrontmostFocused, backgroundFrontmostNotFocused, backgroundNotFrontmost);
        UITextArea.SetColors(foregroundFrontmostFocused, foregroundFrontmostNotFocused, foregroundNotFrontmost, backgroundFrontmostFocused, backgroundFrontmostNotFocused, backgroundNotFrontmost);
        UIBorder.SetColors(foregroundFrontmostFocused, foregroundFrontmostNotFocused, foregroundNotFrontmost, backgroundFrontmostFocused, backgroundFrontmostNotFocused, backgroundNotFrontmost);
    }
}