using System.Numerics;
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;
public class Program
{
    public const int screenWidth = 600;
    public const int screenHeight = 600;
    static void Main()
    {
        Font font = GetFontDefault();       // Get default system font
        Game game = new Game();
        game.Print();

        InitWindow(screenWidth, screenHeight, "Cowboy Game (Cybernaboo)");
        SetTargetFPS(60);
        // Mie en pause du programme en attendant qu'une touche clavier soit appuyée
        // Console.WriteLine("une touche pour continuer");
        // Console.ReadKey(true);

        while (!WindowShouldClose())
        {
            // if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            // {
            //     int mouseX = GetMouseX();
            //     int mouseY = GetMouseY();
            //     System.Console.WriteLine($"Clic souris aux coordonnées : ({mouseX}, {mouseY})");
            // }
            BeginDrawing();
            ClearBackground(WHITE);
            DrawTextEx(font, "Score", new Vector2(5.0f, 5.0f), 25, 2, GREEN);
            DrawRectangle(0, 30, screenWidth, 2, BLACK);
            game.Draw();
            EndDrawing();
        }
        UnloadFont(font);    // TTF Font unloading
        CloseWindow();
    }
}
