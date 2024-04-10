public interface ITickable
{
  public bool CanTick() { return false; }

  /// <summary>
  /// Called inbetween frames
  /// </summary>
  /// <param name="deltaTime">time since the last tick in miniseconds</param>
  public void Tick(long deltaTime)
  {

  }
}