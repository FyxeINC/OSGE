public class UIObject : GameObject
{    
    #region Constructors
    public UIObject(string name, Point position) 
        : base(name, position)
    {
    }

    public UIObject(string name, int x, int y) 
        : base(name, x, y)
    {
    }
    #endregion

    public int Width;
    public int Height;
    public bool UseOffsetTopBottom;
    public bool UseOffsetLeftRight;
    public int OffsetTop;
    public int OffsetBottom;
    public int OffsetLeft;
    public int OffsetRight;


    public Rect Rectangle;

    public Rect GetRect()
    {
        Point position = GetPosition();
        int width = Width;
        int height = Height;

        if (UseOffsetTopBottom)
        {
            if (Parent != null)
            {
                height = (Parent as UIObject).GetRect().Height;

                Position.Y += OffsetTop;
                height -= OffsetTop;
                height -= OffsetBottom;
            }
        }

        if (UseOffsetLeftRight)
        {
            if (Parent != null)
            {
                height = (Parent as UIObject).GetRect().Height;   
                
                Position.X += OffsetLeft;
                width -= OffsetLeft;
                width -= OffsetRight;
            }
        }

        return new Rect (position, width, height);
    }

    public override void Draw()
    {
        base.Draw();
        Rect rect = GetRect();

        Random rand = new Random();
        int randInt = rand.Next(0,2);

        ConsoleColor color = ConsoleColor.Blue;

        switch(randInt)
        {
            case 0:
                color = ConsoleColor.Blue;
                break;
            case 1:
                color = ConsoleColor.Red;
                break;                
            case 2:
                color = ConsoleColor.Yellow;
                break;
        }

        for (int x = rect.X; x < rect.Width; x++)
        {
            for (int y = rect.Y; y < rect.Height; y++)
            {
                Display.Draw(x,y,' ', color, color);
            }
        }
    }
}