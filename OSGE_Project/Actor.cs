public class Actor : GameObject
{
	Controller m_Controller;

    public void SetController(Controller newController)
    {
        m_Controller = newController;
        OnSetController(m_Controller);
    }

    protected virtual void OnSetController(Controller newController)
    {

    }
}