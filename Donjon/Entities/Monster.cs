using System;
using System.Linq;

namespace Donjon.Entities
{

    abstract class Monster : Creature
    {
        protected Monster(string name, string symbol, ConsoleColor color) : base(name, symbol, color)
        {
        }

        public override ConsoleColor Color {
            get {
                return IsDead 
                    ? ConsoleColor.Gray 
                    : IsAggressive 
                        ? ConsoleColor.Red
                        : base.Color;
            }
        }

        public bool IsAggressive { get; set; }

        internal bool Action(Map map)
        {
            if (IsDead) return false;
            if (!IsAggressive) return false;
            var hero = map.Creatures.FirstOrDefault(c => c is Hero) as Hero;
            var dx = hero.Position.X - Position.X;
            var dy = hero.Position.Y - Position.Y;
            if (Math.Abs(dx) > 1 || Math.Abs(dy) > 1) return false;
            return Attack(hero);            
        }
    }
}
