public class UIElement : IDrawable
{
  public UIParent m_Parent;
  public Transform m_Transform;
  
  public bool GetIsFocused() => m_Parent.GetIsFocused();
  public virtual void OnFocused() {}
  public virtual void OnUnfocused() {}

  public override void Draw() {}

}