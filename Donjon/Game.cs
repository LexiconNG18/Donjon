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
            bool gameInProgress = true;
            do
            {
                Draw();
                var key = Console.ReadKey(intercept: true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow: MoveHero(Direction.N); break;
                    case ConsoleKey.DownArrow: MoveHero(Direction.S); break;
                    case ConsoleKey.LeftArrow: MoveHero(Direction.W); break;
                    case ConsoleKey.RightArrow: MoveHero(Direction.E); break;
                    case ConsoleKey.Q: gameInProgress = false; break;
                    default: break;
                }
                Draw();
                // Game actions

            } while (gameInProgress);
            Draw();
            Console.WriteLine("Game Over");
        }

        private void MoveHero(Position direction)
        {
            Cell origin = map.Cell(hero.Position);

            Position target = new Position
            {
                X = hero.Position.X + direction.X,
                Y = hero.Position.Y + direction.Y
            };

            Cell destination = map.Cell(target);
            map.Move(origin, destination);
        }

        private void Draw()
        {
            Console.Clear();
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    var cell = map.Cell(x, y);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(" " + cell.Symbol);
                }
                Console.WriteLine();
            }
        }

        private void Init()
        {
            map = new Map(10, 10);
            hero = new Hero();
            map.Place(hero, map.Cell(hero.Position));
        }
    }
}