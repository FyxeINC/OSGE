public class PlayerController : IInputHandler
{
    public PlayerController()
    {
        this.RegisterHandler();
    }

    ~PlayerController()
    {
        this.UnregisterHandler();
    }
}