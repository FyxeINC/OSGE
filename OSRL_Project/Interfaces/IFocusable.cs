public interface IFocusable
{
  public string Name {get; set;}
  public bool CanFocus {get; set;}
  public bool Navigate(NavigationDirection direction);
  public void OnFoucsed();
  public void OnUnfocused();
  public Rect GetScreenSpaceRect();

  public void SetFocusRelation(NavigationDirection direction, IFocusable focusable);
}