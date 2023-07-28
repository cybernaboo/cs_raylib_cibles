using System.Numerics;
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;


public class Game
{
    List<Vector2> targetList = new List<Vector2>();

    static int gameWidth = 500;
    static int gameHeight = 620;
    static int targetWidth = 50;
    static int targetHeight = 50;
    private Texture texture;
    private Image image;

    public Game()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        Console.WriteLine("Répertoire en cours : " + currentDirectory);
        string filePath = "./images/food.png"; // Remplacez ceci par le chemin d'accès complet de votre fichier
        if (File.Exists(filePath))
        {
            Console.WriteLine("fichier trouvé");
        }
        image = LoadImage("./images/food.png");
        texture = LoadTextureFromImage(image);
        // UnloadImage(image);
        CreateRandomTarget();
    }
    ~Game()
    {
        UnloadTexture(texture);
    }
    void CreateRandomTarget()
    {
        Vector2 newTarget = new Vector2(GetRandomValue(0, gameWidth - targetWidth), GetRandomValue(0, gameHeight - targetHeight));
        targetList.Add(newTarget);
    }

    public void Print()
    {
        foreach (Vector2 target in targetList)
        {
            Console.WriteLine($"Target : {target}");
        }
    }
    public void Draw()
    {
        foreach (Vector2 target in targetList)
        {
            Color darkGreen = new Color(43, 51, 24, 255);
            // DrawTexture(texture, (int)target.X, (int)target.Y, darkGreen);
            DrawTexture(texture, 650, 650, darkGreen);
        }
    }
}
