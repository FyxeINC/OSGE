using System.Data;

public enum BorderType
{
	solidA,
	solidB,
	solidC,
	solidO,
	solidX,
	solidExclamation,
	solidQuestion,
	doubleLine,
	singleLine,
}

public class UI_Border : UIObject
{
	#region Constructors    
	public UI_Border(BorderType type)
		: base("Border", 0, 0, 0, 0)
	{
		Type = type;
        CurrentAnchorPoint = UIHelper.AnchorStretch;
	}
	#endregion

	BorderType Type;

	public override void Draw()
	{
		Rect rect = GetRect();
		for (int x = 0; x < rect.Width; x++)
		{
			for (int y = 0; y < rect.Height; y++)
			{
				bool canDraw = false;
				char toUse = '?';
				if (x == 0 || y == 0 || x == rect.Width-1 || y == rect.Height-1)
				{
					canDraw = true;

					if (x == 0 && y == 0)                               // Top Left
					{
						toUse = GetCharForBorderType(Type, 4);
					}
					else if (x == 0 && y == rect.Height-1)              // Bottom Left
					{
						toUse = GetCharForBorderType(Type, 6);
					}
					else if (x == rect.Width-1 && y == rect.Height-1)   // Bottom Right
					{
						toUse = GetCharForBorderType(Type, 7);
					}
					else if (x == rect.Width-1 && y == 0)               // Top Right
					{
						toUse = GetCharForBorderType(Type, 5);
					}
					else if (x == 0)                                    // Vertical left
					{
						toUse = GetCharForBorderType(Type, 2);
					}
					else if (x == rect.Width-1)                         // Vertical Right
					{
						toUse = GetCharForBorderType(Type, 3);
					}
					else if (y == 0)                                    // Horizontal top
					{
						toUse = GetCharForBorderType(Type, 0);
					}
					else if (y == rect.Height-1)                         // Horizontal bottom
					{
						toUse = GetCharForBorderType(Type, 1);
					}
				}
				
				if (canDraw)
				{
					DisplayManager.Draw(x + rect.X, y + rect.Y, toUse, GetColorForeground(), GetColorBackground());
				}
			}
		}
		base.Draw();
	}

	/*
		0 - horizontal top
		1 - horizontal bottom
		2 - vertical left
		3 - vertical right
		4 - top left
		5 - top right
		6 - bottom left
		7 - bottom right
	*/
	char GetCharForBorderType(BorderType type, int position)
	{
		char toReturn = ' ';
		switch(type)
		{
			case BorderType.solidA:
				toReturn = '░';
				break;
			case BorderType.solidB:
				toReturn = '▒';
				break;
			case BorderType.solidC:
				toReturn = '▓';
				break;
			case BorderType.solidO:
				toReturn = 'O';
				break;
			case BorderType.solidX:
				toReturn = 'X';
				break;
			case BorderType.solidExclamation:
				toReturn = '!';
				break;
			case BorderType.solidQuestion:
				toReturn = '?';
				break;
			case BorderType.doubleLine:
				if (position == 0)      // horizontal top
				{
					toReturn = '═';
				}
				else if (position == 1) // horizontal bottom
				{
					toReturn = '═';
				}
				else if (position == 2) // vertical left
				{
					toReturn = '║';
				}
				else if (position == 3) // vertical right
				{
					toReturn = '║';
				}
				else if (position == 4) // top left
				{
					toReturn = '╔';
				}
				else if (position == 5) // top right
				{
					toReturn = '╗';
				}
				else if (position == 6) // bottom left
				{
					toReturn = '╚';
				}
				else if (position == 7) // bottom right
				{
					toReturn = '╝';
				}
				break;            
			case BorderType.singleLine:
				if (position == 0)      // horizontal top
				{
					toReturn = '─';
				}
				else if (position == 1) // horizontal bottom
				{
					toReturn = '─';
				}
				else if (position == 2) // vertical left
				{
					toReturn = '│';
				}
				else if (position == 3) // vertical right
				{
					toReturn = '│';
				}
				else if (position == 4) // top left
				{
					toReturn = '┌';
				}
				else if (position == 5) // top right
				{
					toReturn = '┐';
				}
				else if (position == 6) // bottom left
				{
					toReturn = '└';
				}
				else if (position == 7) // bottom right
				{
					toReturn = '┘';
				}
				break;
		}

		return toReturn;
	}
}