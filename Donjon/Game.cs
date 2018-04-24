using Donjon.Entities;
using Donjon.Utilities;
using System;

namespace Donjon
{
    internal class Game
    {
        private Map map;
        private Hero hero;
        private bool gameInProgress;
        private Log log = new Log();

        public Game()
        {
        }

        internal void Run()
        {
            Init();
            gameInProgress = true;
            do
            {
                Draw();
                UserActions();
                Draw();
                GameActions();

            } while (gameInProgress);
            Draw();
            Console.WriteLine("Game Over");
        }

        private void GameActions()
        {
            // Game actions
            foreach (var monster in map.Monsters)
            {
                if (monster.Action(map))
                {
                    System.Threading.Thread.Sleep(500);
                    Draw();
                }
            }
        }

        private void UserActions()
        {
            var acted = false;
            do
            {
                var key = Console.ReadKey(intercept: true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        acted = MoveHero(Direction.N); break;
                    case ConsoleKey.DownArrow:
                        acted = MoveHero(Direction.S); break;
                    case ConsoleKey.LeftArrow:
                        acted = MoveHero(Direction.W); break;
                    case ConsoleKey.RightArrow:
                        acted = MoveHero(Direction.E); break;
                    case ConsoleKey.Q:
                        gameInProgress = false; break;
                }
            } while (!acted && gameInProgress);
        }

        private bool MoveHero(Position direction)
        {
            Cell origin = map.Cell(hero.Position);
            var x = hero.Position.X + direction.X;
            var y = hero.Position.Y + direction.Y;
            
            Cell destination = map.Cell(x, y);
            if (destination == null) return false;
            if (destination.Creature != null)
            {
                return hero.Attack(destination.Creature);
            }
            return map.Move(origin, destination);
        }

        private void Draw()
        {
            //Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    var cell = map.Cell(x, y);
                    Console.ForegroundColor = cell.Color;
                    Console.Write(" " + cell.Symbol);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"HP: {hero.Health}/{hero.MaxHealth}  Dmg: {hero.Damage}    ");

            var lines = Console.WindowHeight - Console.CursorTop - 2;

            foreach (string line in log.GetLast(lines)) {
                Console.WriteLine(line);
            }
        }

        private void Init()
        {
            map = new Map(10, 10, log);
            hero = new Hero();
            log.Clear();
            Console.Clear();
            log.Add("Welcome to the Donjon!");
            map.Place(hero, map.Cell(hero.Position));
            map.Place(new Goblin(), map.Cell(5, 7));
            map.Place(new Goblin(), map.Cell(7, 5));
            map.Place(new Goblin(), map.Cell(3, 3));
        }
    }
}