using System;

namespace Donjon.Entities.Creatures
{
    internal class Goblin : Monster
    {
        protected Goblin(string name, string symbol, ConsoleColor color, int health) : base(name, symbol, color, health)
        {
        }

        public Goblin() : this("Goblin", "G", ConsoleColor.Green, health: 15)
        {
            BaseDamage = 7;
        }

        internal override bool Action()
        {
            if (IsDead || !IsAggressive) return false;
            return Hunt(Level.Hero);
        }

        private bool Hunt(Creature target)
        {
            var movement = target.Position - Position;
            if (movement.WalkingDistance > 4) return false;
            Log.Add($"The {Name} pursues the hero");
            return Walk(movement.Step);
        }
    }

    internal class Orc : Goblin
    {
        public Orc() : base("Orc", "O", ConsoleColor.Green, health: 20)
        {
            IsAggressive = true;
            BaseDamage = 10;
        }
    }
}