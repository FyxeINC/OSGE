public interface ITickable
{
  public bool CanTick {get; set;}

  public void SetTickable(bool canTick)
  {
    if (canTick == CanTick)
    {
        return;
    }

    CanTick = canTick;

    if (CanTick)
    {
        TimeManager.Register(this);
    }
    else
    {
        TimeManager.Unregister(this);
    }
  }

  /// <summary>
  /// Called inbetween frames
  /// </summary>
  /// <param name="deltaTime">time since the last tick in miniseconds</param>
  public void Tick(long deltaTime)
  {

  }
}