using System.Numerics;
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace CowboyGame
{
    public class Program
    {
        public const int screenWidth = 600;
        public const int screenHeight = 600;
        public static Image image = LoadImage("images/cible.png");
        private static double lastUpdateTime = 0;

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

            Texture texture = LoadTextureFromImage(image);
            UnloadImage(image);

            Font font = GetFontDefault();       // Get default system font
            Game game = new();
            game.Print();

            while (!WindowShouldClose())
            {
                if (!game.running)
                {
                    DrawTextEx(font, "Game Over", new Vector2(200.0f, 200.0f), 50, 2, BLACK);
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
                    DrawTextEx(font, "Score " + game.score, new Vector2(5.0f, 5.0f), 25, 2, BLACK);
                    DrawRectangle(0, 30, screenWidth, 2, BLACK);
                    game.Draw(texture);
                }
                EndDrawing();
            }
            UnloadTexture(texture);
            UnloadFont(font);    // TTF Font unloading
            CloseWindow();
        }
    }
}
