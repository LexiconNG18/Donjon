using System;

namespace Donjon.Entities.Creatures {
    class Goblin : Monster {
        private int aggroRange = 4;

        protected Goblin(string name, string symbol, ConsoleColor color, int health) :
            base(name, symbol, color, health) { }

        public Goblin() : this("Goblin", "G", ConsoleColor.Green, health: 15) => BaseDamage = 7;

        public override bool Action() {
            if (IsDead || !IsAggressive) return false;
            return Hunt(Level.Hero);
        }

        private bool Hunt(Creature target) {
            var movement = target.Position - Position;
            if (movement.WalkingDistance > aggroRange) return false;
            Log.Add($"The {Name} pursues the hero");
            return Walk(movement.Step);
        }
    }
}