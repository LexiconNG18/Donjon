using Donjon.Entities;
using Donjon.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    case ConsoleKey.P:
                        acted = PickUp(); break;
                    case ConsoleKey.D:
                        acted = Drop(); break;
                    case ConsoleKey.I:
                        acted = Inventory(); break;
                    case ConsoleKey.Q:
                        gameInProgress = false; break;
                }
                Draw();
            } while (!acted && gameInProgress);
        }

        private bool Inventory()
        {
            string heading = hero.Backpack.Any()
                ? "Your Backpack contains:"
                : "Your Backpack is empty";
            log.Add(heading);
            foreach (var item in hero.Backpack) log.Add("   " + item.Name);
            return false;
        }

        private bool Drop()
        {
            if (hero.Backpack.Any())
            {
                var item = hero.Backpack.First();
                hero.Backpack.Remove(item);
                map.Cell(hero.Position).Items.Add(item);
                log.Add($"You dropped the {item.Name}");
            }
            return false;
        }

        private bool PickUp()
        {
            var cell = map.Cell(hero.Position);
            var items = cell.Items;
            if (items.Any() && !hero.Backpack.IsFull)
            {
                var item = items[0];
                hero.Backpack.Add(item);
                items.Remove(item);
                log.Add($"You pick up the {item.Name}");
                return true;
            }
            return false;
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
            bool moved = map.Move(origin, destination);
            if (moved)
            {
                var items = map.Cell(hero.Position).Items;
                if (items.Any())
                {
                    var stuff = string.Join(", ", items.Select(i => i.Name));
                    log.Add($"You see: {stuff}");
                }
            }
            return moved;
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
                    IDrawable appearance = map.Cell(x, y).Appearance;
                    Console.ForegroundColor = appearance.Color;
                    Console.Write(" " + appearance.Symbol);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"HP: {hero.Health}/{hero.MaxHealth}  Dmg: {hero.Damage}    ");

            var lines = Console.WindowHeight - Console.CursorTop - 2;

            foreach (string line in log.GetLast(lines))
            {
                Console.Write(new string(' ', Console.WindowWidth - 1));
                Console.CursorLeft = 0;
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
            map.Place(Item.Coin(), map.Cell(2, 2));
            map.Place(Item.Coin(), map.Cell(2, 3));
            map.Place(Item.Coin(), map.Cell(3, 3));
        }
    }
}