public interface ITickable
{
  bool CanTick {get; set;}

  /// <summary>
  /// Called inbetween frames
  /// </summary>
  /// <param name="deltaTime">time since the last tick in miniseconds</param>
  public void Tick(long deltaTime)
  {

  }
}