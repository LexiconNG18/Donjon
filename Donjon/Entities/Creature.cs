using Donjon.Utilities;
using System;

namespace Donjon.Entities
{
    internal abstract class Creature : IDrawable
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public virtual ConsoleColor Color { get; set; }

        public Position Position { get; set; }
        public int Health { get; set; } = 15;
        public int MaxHealth { get; set; } = 15;
        public int Damage { get; set; }
        public bool IsDead => Health <= 0;
        public Log Log { get; set; }

        protected Creature(string name, string symbol, ConsoleColor color)
        {
            Name = name;
            Symbol = symbol;
            Color = color;
        }

        public virtual bool Attack(Creature opponent) {
            if (opponent.IsDead) return false;
            Log.Add($"The {Name} attacks the {opponent.Name} ({opponent.Health})");
            opponent.Defend(Damage);
            return true;
        }

        protected virtual void Defend(int damage)
        {
            Health -= damage;
            string message = $"The {Name} takes {damage} damage";            
            if (IsDead) message += ($" and dies");
            Log.Add(message);
        }
    }
}