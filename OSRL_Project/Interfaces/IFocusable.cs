public interface IFocusable
{
  public bool CanFocus {get; set;}
  public bool Navigate(NavigationDirection direction);
  public void OnFoucsed();
  public void OnUnfocused();
}