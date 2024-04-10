

public class UI_Bar : UIObject
{
    public UI_Bar(int x, int y, int width, int height)
        : base("bar",x,y,width,height)
    {
		UISolidFill_Background = new UI_SolidFill (' ');
        UISolidFill_Background.Name = Name + "_FillBackground";
		UISolidFill_Background.SetAnchorPoint(AnchorPointHorizonal.stretch, AnchorPointVertical.stretch);
        UISolidFill_Background.SetLocalPosition(0,0);
        
		UISolidFill_Foreground = new UI_SolidFill (' ');
        UISolidFill_Foreground.Name = Name + "_FillForeground";
		UISolidFill_Foreground.SetAnchorPoint(AnchorPointHorizonal.stretch, AnchorPointVertical.stretch);
        UISolidFill_Foreground.SetLocalPosition(0,0);

        this.AddChild(UISolidFill_Background);
        this.AddChild(UISolidFill_Foreground);
    }

	UI_SolidFill UISolidFill_Background;
	UI_SolidFill UISolidFill_Foreground;

    public Direction BarFillDirection = Direction.right;
    float CurrentFillPercentage = -1.0f;

    public override void SetColors(ConsoleColor foreground, ConsoleColor? background)
    {
        base.SetColors(foreground, background);
        UISolidFill_Foreground.SetColors(foreground, foreground);
        // TODO - error prone
        UISolidFill_Background.SetColors(background.Value, background);
    }

    public void SetFillPercentage(float percentage)
    {
        // if (percentage == CurrentFillPercentage)    
        // {
        //     return;
        // }
        percentage = Math.Clamp(percentage, 0.0f, 1.0f);

        CurrentFillPercentage = percentage;


        //float inversePercent = (1.0f - percentage) * 1f;
        int offset = 0; 

        if (BarFillDirection == Direction.up)
        {
            offset = (int)Math.Round(((1.0f - percentage) * (float)GetScreenSpaceRect().Height));
            UISolidFill_Foreground.SetOffset(offset,0,0,0);
        }
        else if (BarFillDirection == Direction.down)
        {
            offset = (int)Math.Round(((1.0f - percentage) * (float)GetScreenSpaceRect().Height));
            UISolidFill_Foreground.SetOffset(0,offset,0,0);
        }
        else if (BarFillDirection == Direction.left)
        {
            offset = (int)Math.Round(((1.0f - percentage) * (float)GetScreenSpaceRect().Width));
            UISolidFill_Foreground.SetOffset(0,0,offset,0);
        }
        else if (BarFillDirection == Direction.right)
        {
            offset = (int)Math.Round(((1.0f - percentage) * (float)GetScreenSpaceRect().Width));
            UISolidFill_Foreground.SetOffset(0,0,0,offset);
        }
    }

    public void SetFillDirection(Direction newDirection)
    {
        BarFillDirection = newDirection;
        SetFillPercentage(CurrentFillPercentage);
    }

    public override void SetParent(GameObject newParent)
    {
        base.SetParent(newParent);
        SetFillPercentage(CurrentFillPercentage);
    }
}
