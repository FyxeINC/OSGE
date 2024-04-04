public interface ITickable
{
  public bool CanTick {get; set;}
  public void Tick(float deltaTime);
}