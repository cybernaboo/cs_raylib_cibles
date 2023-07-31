using System.Numerics;
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace CowboyGame
{
    public class Program
    {
        public const int screenWidth = 500;
        public const int screenHeight = 365;
        static readonly int titleHeight = 35;
        static Image imageCible = LoadImage("images/cible2.png");
        static Image imageWallPaper = LoadImage("images/westernwallpaper.png");
        static Image imageTitle = LoadImage("images/woodboard.png");
        private static double lastUpdateTime = 0;
        private static double frequencyTime = 0;
        public static Color transparent = new(255, 255, 255, 128);
        public static double intervalTime = 1;

        private static bool EventTriggered(double Interval, ref double referenceTime)
        {
            double currentTime = GetTime();
            if (currentTime - referenceTime > Interval)
            {
                referenceTime = currentTime;
                return true;
            }
            return false;
        }
        static void Main()
        {
            InitWindow(screenWidth, screenHeight, "Cowboy Game (Cybernaboo)");
            SetTargetFPS(60);

            Texture textureCible = LoadTextureFromImage(imageCible);
            UnloadImage(imageCible);
            Texture textureWallPaper = LoadTextureFromImage(imageWallPaper);
            UnloadImage(imageWallPaper);
            Texture textureTitle = LoadTextureFromImage(imageTitle);
            UnloadImage(imageTitle);

            Font font = LoadFont("fonts/Western.ttf");

            Game game = new();
            lastUpdateTime = GetTime();
            frequencyTime = GetTime();
            // game.Print();

            while (!WindowShouldClose())
            {
                UpdateMusicStream(game.music);
                if (!game.running)
                {
                    if (!game.firstGame)
                    {
                        DrawTextEx(font, "GAME OVER", new Vector2(180.0f, 310.0f), 50, 2, YELLOW);
                    }
                    else
                    {
                        ClearBackground(WHITE);
                        DrawTexture(textureTitle, 0, 0, WHITE);
                        DrawTexture(textureWallPaper, 0, 0 + titleHeight, WHITE);
                        DrawTextEx(font, "SCORE " + game.score, new Vector2(20.0f, 10.0f), 20, 2, BLACK);
                        DrawTextEx(font, "HI-SCORE " + Game.gameHighScore, new Vector2(320.0f, 10.0f), 20, 2, WHITE);
                        DrawTextEx(font, "5 targets and you're a dead man", new Vector2(15.0f, 310.0f), 30, 2, YELLOW);
                    }
                    DrawTextEx(font, "PRESS ENTER TO START", new Vector2(80.0f, 150.0f), 25, 2, WHITE);
                    if (IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        game.Reset();
                    }
                }
                else
                {
                    if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
                    {
                        int mouseX = GetMouseX();
                        int mouseY = GetMouseY();
                        game.CheckTarget(mouseX, mouseY);
                    }
                    BeginDrawing();
                    if (EventTriggered(intervalTime, ref lastUpdateTime))
                        game.Update();
                    if (EventTriggered(5, ref frequencyTime))
                        intervalTime *= 0.9;
                    ClearBackground(WHITE);
                    DrawTexture(textureTitle, 0, 0, WHITE);
                    DrawTexture(textureWallPaper, 0, 0 + titleHeight, WHITE);
                    DrawTextEx(font, "SCORE " + game.score, new Vector2(20.0f, 10.0f), 20, 2, BLACK);
                    DrawTextEx(font, "HI-SCORE " + Game.gameHighScore, new Vector2(320.0f, 10.0f), 20, 2, WHITE);
                    game.Draw(textureCible);
                }
                EndDrawing();
            }
            UnloadTexture(textureWallPaper);
            UnloadTexture(textureCible);
            UnloadTexture(textureTitle);
            UnloadFont(font);
            CloseWindow();
        }
    }
}
