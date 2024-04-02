public class UIElement : IRenderable
{
  public UIParent m_Parent;

  public bool GetIsFocused() => m_Parent.GetIsFocused();
  public virtual void OnFocused() {}
  public virtual void OnUnfocused() {}

  public override void Render() {}

}