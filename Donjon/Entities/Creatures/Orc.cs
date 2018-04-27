using System;

namespace Donjon.Entities.Creatures {
    class Orc : Goblin {
        public Orc() : base("Orc", "O", ConsoleColor.Green, health: 20) {
            IsAggressive = true;
            BaseDamage   = 10;
        }
    }
}