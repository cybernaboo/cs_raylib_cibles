using System.Numerics;
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;
using System.IO;
using System.Text.Json;


namespace CowboyGame
{

    public class Game
    {
        List<Vector2> targetList = new List<Vector2>();

        // static int gameWidth = 500;
        // static int gameHeight = 368;
        static int titleHeight = 35;
        static int targetWidth = 50;
        static int targetHeight = 50;
        public bool running = false;
        public bool firstGame = true;
        public int score = 0;
        Sound gunShotSound;
        Sound gameoverSound;
        public Music music;

        public static int gameHighScore = ReadHighScore("data/highscore.json");
        public Game()
        {
            InitAudioDevice();
            music = LoadMusicStream("Sounds/westernmusic.mp3");
            PlayMusicStream(music);
            gunShotSound = LoadSound("./sounds/gunshot.wav");
            gameoverSound = LoadSound("./sounds/gameover.wav");
            CreateRandomTarget();
        }
        ~Game()
        {
            UnloadMusicStream(music);
            UnloadSound(gunShotSound);
            UnloadSound(gameoverSound);
            CloseAudioDevice();
        }
        void CreateRandomTarget()
        {
            Vector2 newTarget = new Vector2(GetRandomValue(0, Program.screenWidth - targetWidth), GetRandomValue(titleHeight, Program.screenHeight - targetHeight));
            targetList.Add(newTarget);
        }

        public void Print()
        {
            foreach (Vector2 target in targetList)
            {
                Console.WriteLine($"Target : {target}");
            }
        }
        public void Draw(Texture texture)
        {
            foreach (Vector2 target in targetList)
            {
                DrawTexture(texture, (int)target.X, (int)target.Y, Program.transparent);
                // }
            }
        }
        public void Update()
        {
            if (targetList.Count < 5)
                CreateRandomTarget();
            else
            {
                GameOver();
            }
        }
        public void GameOver()
        {
            running = false;
            PlaySound(gameoverSound);
            UpdateHighScore("data/highscore.json", score);
        }

        public void Reset()
        {
            running = true;
            targetList.Clear();
            // gameHighScore = ReadHighScore("data/highscore.json");
            Program.intervalTime = 1;
            score = 0;
        }

        public void CheckTarget(int x, int y)
        {
            foreach (Vector2 target in targetList)
            {
                if (x >= target.X && x <= target.X + targetWidth && y >= target.Y && y <= target.Y + targetHeight)
                {
                    targetList.Remove(target);
                    score += 10;
                    PlaySound(gunShotSound);
                    break;
                }
            }
        }

        public static int ReadHighScore(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                HighScore? highScore = new HighScore();
                highScore = JsonSerializer.Deserialize<HighScore>(jsonData);
                if (highScore is not null)
                    return highScore.highScoreValue;
                else
                    return 0;
            }
            else
            {
                return 0; // Return 0 if the file doesn't exist or cannot be read
            }
        }
        public static void UpdateHighScore(string filePath, int newScore)
        {
            // int currentHighScore = ReadHighScore(filePath);

            // if (newScore > currentHighScore)
            if (newScore > gameHighScore)
            {
                HighScore highScore = new HighScore { highScoreValue = newScore };
                string jsonData = JsonSerializer.Serialize(highScore);
                File.WriteAllText(filePath, jsonData);
                gameHighScore = newScore;
            }
        }
    }
}
