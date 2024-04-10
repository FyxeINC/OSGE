public class UI_Button : UIObject
{
#region Constructors
    public UI_Button(string name, Point screenPosition, int width, int height) 
        : base(name, screenPosition, width, height)
    {
    }
#endregion

    public bool IsEnabled = true;
    public Action OnPress;
    public Action OnHover;
    public Action OnUnhover;
}