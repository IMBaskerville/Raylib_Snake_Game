using System.Numerics;
using static Raylib_cs.Color;
using static Raylib_cs.KeyboardKey;
using static Raylib_cs.Raylib;

namespace Raylib_Snake
{
    class Game
    {
        // Static fields
        public static readonly int SQUARESIZE = 30;

        // Window Fields
        private const int WIDTH = 800;
        private const int HEIGHT = 480;
        private const int FPS = 60;
        private const string TITLE = "Raylib Snake Game";

        // Game fields
        private int _frameCounter;
        private float _totalTime;
        private float _deltaTime;
        private bool _pause;
        private const float BLINK_RESET = 0.5f;
        private float _pauseBlinkTimer;
        private bool _pauseCanDisplay;

        // Text fields
        private const int TEXT_X = 630;
        private const int FONT_SIZE = 20;

        // Game Objects
        private Board _board;
        private Snake _snake;
        private FoodSpawner _food;
        public Game()
        {
            InitWindow(WIDTH, HEIGHT, TITLE);
            SetTargetFPS(FPS);
            InitGame();
            Restart();
            Run();
        }

        private void InitGame()
        {
            _board = new Board();
            _snake = new Snake(SQUARESIZE);
            _food = new FoodSpawner();
        }

        private void Restart()
        {
            _totalTime = 0;
            _deltaTime = 0;
            _frameCounter = 0;
            _pause = false;
            _pauseBlinkTimer = 0;
            _pauseCanDisplay = true;
            _snake.InitSnake();
            FoodSpawn();
        }

        private void Run()
        {
            while (!WindowShouldClose())
            {
                VerifyEvents();
                UpdateDeltaTime();
                if (!_pause)
                {
                    if (_frameCounter % 7 == 0)
                    {
                        Update();
                    }
                }
                Render();  
                UpdateFrameCounter();              
            }

            CloseWindow();
        }

        private void Update()
        {
            _snake.Update();
            VerifyTailHit();
            VerifyIfFoodEaten();
            _snake.CanMove = true;
        }

        private void UpdateDeltaTime()
        {
            _deltaTime = GetFrameTime();
            if (!_pause)
            {
                _totalTime += _deltaTime;
            }
            else
            {
                if (_pauseBlinkTimer >= BLINK_RESET)
                {
                    _pauseBlinkTimer = 0;
                    _pauseCanDisplay = !_pauseCanDisplay;
                }
                _pauseBlinkTimer += _deltaTime;
            }
        }

        private void UpdateFrameCounter()
        {
            // Resetting counter to avoid an overflow
            if (_frameCounter >= 1000)
            {
                _frameCounter = 0;
            }
            _frameCounter++;
        }

        private void VerifyEvents()
        {
            if (_snake.CanMove)
            {
                if (IsKeyPressed(KEY_D) && !_snake.MoveLeft)
                {
                    _snake.MoveLeft = false;
                    _snake.MoveRight = true;
                    _snake.MoveUp = false;
                    _snake.MoveDown = false;
                    _snake.CanMove = false;
                }
                if (IsKeyPressed(KEY_A) && !_snake.MoveRight)
                {
                    _snake.MoveLeft = true;
                    _snake.MoveRight = false;
                    _snake.MoveUp = false;
                    _snake.MoveDown = false;
                    _snake.CanMove = false;
                }
                if (IsKeyPressed(KEY_W) && !_snake.MoveDown)
                {
                    _snake.MoveLeft = false;
                    _snake.MoveRight = false;
                    _snake.MoveUp = true;
                    _snake.MoveDown = false;
                    _snake.CanMove = false;
                }
                if (IsKeyPressed(KEY_S) && !_snake.MoveUp)
                {
                    _snake.MoveLeft = false;
                    _snake.MoveRight = false;
                    _snake.MoveUp = false;
                    _snake.MoveDown = true;
                    _snake.CanMove = false;
                }
            }

            if (IsKeyPressed(KEY_P))
            {
                _pause = !_pause;
                if (!_pause)
                {
                    _pauseCanDisplay = true;
                    _pauseBlinkTimer = 0;
                }
            }

            if (IsKeyPressed(KEY_R))
            {
                Restart();
            }
        }

        private void VerifyTailHit()
        {
            if (_snake.VerifyHit())
            {
                Restart();
            }
        }

        private void VerifyIfFoodEaten()
        {
            Vector2 snakePos = _snake.GetPosition();
            if ((snakePos.X == _food.Pos.X) && (snakePos.Y == _food.Pos.Y))
            {
                _snake.IncreaseScore();
                _snake.AteFood = true;
                FoodSpawn();
            }
        }

        private void FoodSpawn()
        {
            _food.SpawnFood(_snake.GetTail());
        }

        private void Render()
        {
            BeginDrawing();
            ClearBackground(BLACK);

            // Drawing Game Text
            DrawGameText();

            // Drawing game state
            _board.Render();
            _food.Render();
            _snake.Render();

            EndDrawing();
        }

        private void DrawGameText()
        {
            DrawText("Snake Game!", TEXT_X, 12, FONT_SIZE, WHITE);
            DrawText($"Max Score: {_snake.MaxScore}", TEXT_X, 30, FONT_SIZE, WHITE);
            DrawText($"Score: {_snake.GetScore()}", TEXT_X, 48, FONT_SIZE, WHITE);
            DrawText($"Total time: {(int)_totalTime}", TEXT_X, 66, FONT_SIZE, WHITE);
            if (_pause && _pauseCanDisplay)
            {
                DrawText($"Paused", TEXT_X, 84, FONT_SIZE, WHITE);
            }
        }
    }
}
