using System;
using Donjon.Entities.Creatures;
using Donjon.Utils;

namespace Donjon.Entities.Items
{
    internal class TelePotion : Potion
    {
        public TelePotion() : base("Telepotion", "p", ConsoleColor.Magenta)
        {
        }


        protected override Action<Log> ApplyEffect(Hero hero)
        {
            var hx = hero.Position.X;
            var hy = hero.Position.Y;
            var r = new Random();

            var tries = 10;
            bool moved;
            do
            {
                var target = new Position(
                    r.Next(hx - 5, hx + 5),
                    r.Next(hy - 5, hy + 5));
                moved = hero.Level.Move(hero.Position, target);
            } while (!moved && tries-- >= 0);

            return log =>
            {
                var s = "Your surroundings start to spin around. When it stops, you find ";
                s += moved
                    ? "yourself somewhere else"
                    : "that everything is back to normal";
                log.Add(s);
            };
        }
    }
}