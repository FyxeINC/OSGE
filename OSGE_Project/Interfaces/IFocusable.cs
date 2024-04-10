public interface IFocusable
{
  public string Name {get; set;}
  public bool CanFocus {get; set;}
  public bool Navigate(Direction direction);
  public void OnFoucsed();
  public void OnUnfocused();
  public Rect GetScreenSpaceRect();
  public void OnInteract();

  public void SetFocusRelation(Direction direction, IFocusable focusable);
}