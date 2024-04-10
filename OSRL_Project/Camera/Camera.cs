public class Camera : GameObject
{
    GameObject m_ToFollow = null;

    int Width;
    int Height;

    public void SetToFollow(GameObject newToFollow)
    {
        m_ToFollow = newToFollow;
    }

    public override void Draw()
    {
        base.Draw();
        // GetPostion, if ToFollow, use that position
    }
}