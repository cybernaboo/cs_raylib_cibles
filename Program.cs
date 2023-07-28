using System.Numerics;
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace CowboyGame
{
    public class Program
    {
        public const int screenWidth = 500;
        public const int screenHeight = 366;
        static readonly int titleHeight = 32;
        static Image imageCible = LoadImage("images/cible2.png");
        static Image imageWallPaper = LoadImage("images/westernwallpaper.png");
        private static double lastUpdateTime = 0;
        public static Color transparent = new(255, 255, 255, 128);

        private static bool EventTriggered(double Interval)
        {
            double currentTime = GetTime();
            if (currentTime - lastUpdateTime > Interval)
            {
                lastUpdateTime = currentTime;
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

            Font font = GetFontDefault();
            Game game = new();
            game.Print();

            while (!WindowShouldClose())
            {
                UpdateMusicStream(game.music);
                if (!game.running)
                {
                    DrawTextEx(font, "Game Over", new Vector2(250.0f, 310.0f), 50, 2, YELLOW);
                    if (IsKeyPressed(KeyboardKey.KEY_ENTER) && game.running == false)
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
                    if (EventTriggered(0.5))
                    {
                        game.Update();
                    }
                    ClearBackground(WHITE);
                    DrawTexture(textureWallPaper, 0, 0 + titleHeight, WHITE);
                    DrawTextEx(font, "Score " + game.score, new Vector2(5.0f, 5.0f), 25, 2, BLACK);
                    DrawRectangle(0, 30, screenWidth, 2, BLACK);
                    game.Draw(textureCible);
                }
                EndDrawing();
            }
            UnloadTexture(textureCible);
            UnloadFont(font);
            CloseWindow();
        }
    }
}
