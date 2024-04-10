public class UI_Button : UIObject
{
#region Constructors
    public UI_Button(string name, int x, int y, int width, int height) 
        : base(name, x, y, width, height)
    {
        SetCanFocus(true);
    }
    public UI_Button(string name, Point screenPosition, int width, int height) 
        : base(name, screenPosition, width, height)
    {
        SetCanFocus(true);
    }
#endregion

    public Action OnPress;

    public override void OnFoucsed()
    {
        base.OnFoucsed();
    }

    public override void OnInteract()
    {
        base.OnInteract();
        OnPress();
    }
}