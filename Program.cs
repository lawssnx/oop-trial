using System;
using System.Runtime.InteropServices;
//Console.WindowHeight = Console.BufferHeight = Console.LargestWindowHeight;
//Console.WindowWidth = Console.BufferWidth = Console.LargestWindowWidth;

namespace Space
{
    public class CMain
    {
        public static void Main(string[] args)
        {
            GameMain game = new GameMain();
            Console.CursorVisible = false; 
            Console.Clear();
            game.Play();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"Game Over");
        }
    }
    
    public class GameMain : ConsoleClass
    {
        bool gameover;
        private int Delay = 20;
        private SpaceShip ship;
        public GameMain()
        {
            
            gameover = false;
            ship = new SpaceShip();
        }

        public void Play()
        {
            while (!gameover)
            {
                Mechanic();
                Delayy();
                ship.ReadKey();
                Moving();
            }
            
        }
        private void Mechanic()
        {
            ship.Mechanic();
        }
        private void Moving()
        {
            ship.Moving();
        }
        private void Delayy()
        {
            System.Threading.Thread.Sleep(Delay);
        }
    }
    public class Coordinates
    {

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
                  
        public int X { get; set; }
        public int Y { get; set;  }
    }
    public class ObjectBasse : ConsoleClass
    {
        public Coordinates _coordinate;
        public int counter;
        public ObjectBasse(Coordinates coordinate)
        {
            _coordinate = coordinate;
           counter = 0;
        }

    }
    public class ConsoleClass
    {
       public void S(int x, int y, string str)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(str);

        }
        public void SetColor(ConsoleColor color)
        {
            Console.ForegroundColor=color;
        }

        public void SColor(int x, int y, ConsoleColor color, string str)
        {
            SetColor(color);
            S(x, y , str); 
        }
    }

    public class SpaceShip : ObjectBasse
    {
        private string[] art;
        private const int shipH = 5;
        private const int shipW = 7;
        private static int LocationX = 55;
        private static int LocationY = 55;
        private const int LeftEdge = 0;
        private const int RightEdge = 60;
        private Move move;
        public SpaceShip() : base(new Coordinates(LocationX, LocationY))
        {
            art = new string[5]
            {
                "   ^   ",
                " #   # ",
                "|^| |^|",
                "|^| |^|",
                "#######",
            };
            move = Move.Stop;
        }
       
        public void Mechanic()
        {
            for (int i = 0; i < shipH; i++)
                for (int j = 0; j < shipW; j++)
                {
                    //Console.SetCursorPosition(j + _coordinate.X, i + _coordinate.Y);
                    //Console.Write(art[i][j].ToString());
                    S(_coordinate.X + j, _coordinate.Y + i, art[i][j].ToString());
                }
        }

        public void ReadKey()
        {
            ConsoleKeyInfo info = new ConsoleKeyInfo(); 
            while (Console.KeyAvailable)
            {
                info = Console.ReadKey(true);
            }
            
            switch (info.Key)
            {
                case ConsoleKey.A:
                    move = Move.lEFT;
                    break;
                case ConsoleKey.D:
                    move = Move.rIGHT;
                    break;
                case ConsoleKey.S:
                    move = Move.Stop;
                    break;
                default:
                    break;
            }
        }
        public void Moving()
        {
            counter++;
            if (counter < 2)
                return;
            counter = 0;
            if (move == Move.lEFT && _coordinate.X > LeftEdge)
            {
                _coordinate.X--;
                Console.Clear();
                
            }
            if (move == Move.rIGHT && _coordinate.Y < RightEdge)
            {
                Console.Clear();
                _coordinate.X++;
            }


        }
    }
    public enum Move
    {
        lEFT,
        rIGHT,
        Stop
    }
public class Bullet : ObjectBasse
{
    private static int LocationY = 55;
    public Bullet(int x) 
            : base(new Coordinates(x + 5, LocationY ))
    {

    }
    public void Appear()
    {
            S(_coordinate.X, _coordinate.Y, "|");
    }
    public void Unappear()
        {
            S(_coordinate.X, _coordinate.Y, " ");
        }
    public void Moving()
    {
            Unappear();
            _coordinate.Y--;
    }
    public int GetY()
        { 
            return _coordinate.Y;
        }
    }
    public class BulletMake
    {
        private List<Bullet> _bullets = new List<Bullet>();
        private int numbOfBullet;
        private int maxBullet;
        public BulletMake()
        {
            numbOfBullet = 0;
            maxBullet = 3;
        }
        public void Appear()
        {
            foreach (var beam in _bullets)
                beam.Appear();
        }
        public void Add(int x)
        {
            if (numbOfBullet >= maxBullet)
                return;
            _bullets.Add(new Bullet(x));    
        }
        public void Move()
        {

        }
    }

}
