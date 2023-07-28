using System.Numerics;
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

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
        public bool running = true;
        public int score = 0;
        Sound gunShotSound;
        Sound gameoverSound;
        public Music music;

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
        }

        public void Reset()
        {
            running = true;
            targetList.Clear();
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
    }
}
