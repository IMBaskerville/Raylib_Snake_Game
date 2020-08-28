using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

namespace Raylib_Snake
{
	class Board
	{
		public const int COLS = 20;
		public const int ROWS = 15;
		public Board()
		{
			// Might be used later on
		}

		public void Render()
		{
			// Draw vertical lines
			for (int i = 0; i <= COLS; i++)
			{
				int xPos = i * Game.SQUARESIZE;
				int yStartPos = 0;
				int yEndPos = ROWS * Game.SQUARESIZE;
				DrawLine(xPos, yStartPos, xPos, yEndPos, WHITE);
			}

			// Draw Horizontal lines
			for (int i = 0; i <= ROWS; i++)
			{
				int yPos = i * Game.SQUARESIZE;
				int xStartPos = 0;
				int xEndPos = COLS * Game.SQUARESIZE;
				DrawLine(xStartPos, yPos, xEndPos, yPos, WHITE);
			}
		}
	}
}