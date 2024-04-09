using System.Data;

public class UI_SolidFill : UIObject
{
	#region Constructors    
	public UI_SolidFill(char character = ' ') 
		: base("Fill", 0, 0, 0, 0)
	{
	  Character = character;
      CurrentAnchorPoint = UIHelper.AnchorStretch;
	}
	#endregion

	char Character;

	public override void Draw()
	{
		Rect rect = GetRect();
		for (int x = 0; x < rect.Width; x++)
		{
			for (int y = 0; y < rect.Height; y++)
			{
				DisplayManager.instance.Draw(x + rect.X, y + rect.Y, Character, GetColorForeground(), GetColorBackground());
			}
		}
		base.Draw();
	}
}