using System;

namespace Donjon.Entities.Creatures {
    class Troll : Monster {
        public Troll() : base("Troll", "T", ConsoleColor.Green, health: 30) { BaseDamage = 15; }

        public override bool Action() => base.Action() || Regenerate();

        private bool Regenerate() {
            if (IsDead || Health >= MaxHealth) return false;
            Health += MaxHealth / 5;
            Log.Add($"The {Name} regenerates some health");
            return true;
        }
    }
}