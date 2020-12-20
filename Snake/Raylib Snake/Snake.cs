using System.Collections.Generic;
using System.Numerics;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

namespace Raylib_Snake
{
    class Snake
    {
        private List<Vector2> _tail;
        private Vector2 _position;
        private readonly int _size;
        private int _score;

        public bool MoveLeft { get; private set; }
        public bool MoveRight { get; private set; }
        public bool MoveUp { get; private set; }
        public bool MoveDown { get; private set; }
        public bool CanMove { get; set; }
        public bool AteFood { get; set; }
        public int MaxScore { get; private set; }

        public Snake(int size)
        {
            _tail = new List<Vector2>();
            _size = size;
            InitSnake();
        }

        public void InitSnake()
        {
            MoveLeft = false;
            MoveRight = false;
            MoveUp = false;
            MoveDown = false;
            CanMove = true;
            AteFood = false;
            _score = 0;
            _position = new Vector2(3 * _size, 0);
            _tail.Clear();
        }

        public void Update()
        {
            MoveSnake();
            RepositionSnake();
            _tail.Add(_position);
            RemoveTail();
        }

        public void Render()
        {
            // Drawing snake tail
            for (int i = 0; i < _tail.Count; i++)
            {
                int x = (int)_tail[i].X;
                int y = (int)_tail[i].Y;
                DrawRectangle(x, y, _size, _size, SKYBLUE);
            }

            // Drawing snake head
            int sX = (int)_position.X;
            int sY = (int)_position.Y;
            DrawRectangle(sX, sY, _size, _size, DARKBLUE);
        }

        public void IncreaseScore()
        {
            _score += 1;
            if (_score > MaxScore)
            {
                MaxScore = _score;
            }
        }

        public string GetScore()
        {
            return _score.ToString();
        }

        public List<Vector2> GetTail()
        {
            return _tail;
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        public bool VerifyHit()
        {
            for (int i = 0; i < _tail.Count; i++)
            {
                if (i != (_tail.Count - 1))
                {
                    if ((_tail[i].X == _position.X) && (_tail[i].Y == _position.Y))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void ChangeMovementDirection(string direction)
        {
            MoveLeft = false;
            MoveRight = false;
            MoveUp = false;
            MoveDown = false;
            CanMove = false;

            SetDirection(direction);
        }

        private void MoveSnake()
        {
            if (MoveLeft)
            {
                _position.X -= _size; 
            }
            if (MoveRight)
            {
                _position.X += _size;
            }
            if (MoveUp)
            {
                _position.Y -= _size;
            }
            if (MoveDown)
            {
                _position.Y += _size;
            }
        }

        private void RepositionSnake()
        {
            // Outside of bounds horizontally
            if (_position.X > (Board.COLS - 1) * _size)
            {
                _position.X = 0;
            }
            if (_position.X < 0 )
            {
                _position.X = (Board.COLS - 1) * _size;
            }
            // Outside of bounds vertically
            if (_position.Y > (Board.ROWS - 1) * _size)
            {
                _position.Y = 0;
            }
            if (_position.Y < 0 )
            {
                _position.Y = (Board.ROWS - 1) * _size;
            }
        }

        private void RemoveTail()
        {
            if (!AteFood && _tail.Count > 1)
            {
                _tail.RemoveAt(0);
            } 
            else
            {
                AteFood = false;
            }
        }

        private void SetDirection(string direction)
        {
            switch (direction)
            {
                case "Up":
                {
                    MoveUp = true;
                    break;
                }
                case "Down":
                {
                    MoveDown = true;
                    break;
                }
                case "Left":
                {
                    MoveLeft = true;
                    break;
                }
                case "Right":
                {
                    MoveRight = true;
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
    }
}