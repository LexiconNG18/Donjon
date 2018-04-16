using System;

namespace Donjon
{
    internal class Game
    {
        private Map map;
        private Hero hero;

        public Game()
        {
        }

        internal void Run()
        {
            Init();
            bool gameInProgress = false;
            do
            {
                Draw();
                var key = Console.ReadKey(intercept: true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow: MoveHero(); break;
                    case ConsoleKey.DownArrow: MoveHero(); break;
                    case ConsoleKey.LeftArrow: MoveHero(); break;
                    case ConsoleKey.RightArrow: MoveHero(); break;
                    default: break;
                }
                Draw();
                // Game actions
                
            } while (gameInProgress);
            Draw();
            Console.WriteLine("Game Over");
        }

        private void MoveHero()
        {

            throw new NotImplementedException();
        }

        private void Draw()
        {
            Console.Clear();
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    var cell = map.Cell(x, y);
                    Console.Write(" " + cell.Symbol);
                }
                Console.WriteLine();
            }
        }

        private void Init()
        {
            map = new Map(10, 10);
            hero = new Hero();
        }
    }
}