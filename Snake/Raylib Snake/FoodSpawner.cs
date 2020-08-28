using System;
using System.Numerics;
using System.Collections.Generic;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

namespace Raylib_Snake
{
    class FoodSpawner
    {
        private readonly Random rand;
        public FoodSpawner()
        {
            rand = new Random();
        }

        public Vector2 Pos { get; private set; }

        public void SpawnFood(List<Vector2> tail)
        {
            bool isSelecting = true;

            while (isSelecting)
            {
                bool isAvailable = true;
                int x = rand.Next(Board.COLS) * Game.SQUARESIZE;
                int y = rand.Next(Board.ROWS) * Game.SQUARESIZE;
                foreach (var t in tail)
                {
                    if ((t.X == x) && (t.Y == y))
                    {
                        isAvailable = false;
                        break;
                    }
                }

                if (isAvailable)
                {
                    Pos = new Vector2(x, y);
                    isSelecting = false;
                }
            }
        }

        public void Render()
        {
            int size = Game.SQUARESIZE;
            int x = (int)Pos.X;
            int y = (int)Pos.Y;
            DrawRectangle(x, y, size, size, RED);
        }
    }
}
