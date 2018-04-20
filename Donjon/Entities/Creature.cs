using System;

namespace Donjon.Entities
{
    internal abstract class Creature
    {
        public string Name { get; set; }
        public virtual ConsoleColor Color { get; set; }
        public string Symbol { get; set; }
        public Position Position { get; set; }
        public int Health { get; set; } = 15;
        public int MaxHealth { get; set; } = 15;
        public int Damage { get; set; }
        public bool IsDead => Health <= 0;

        protected Creature(string name, string symbol, ConsoleColor color)
        {
            Name = name;
            Symbol = symbol;
            Color = color;
        }

        public bool Attack(Creature opponent) {
            if (opponent.IsDead) return false;
            opponent.Health -= Damage;

            if (opponent is Monster)
            {
                (opponent as Monster).IsAggressive = true;
            }
            return true;
        }

    }
}