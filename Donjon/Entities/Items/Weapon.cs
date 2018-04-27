using System;

namespace Donjon.Entities.Items {
    class Weapon : Item {
        private Weapon(string name, string symbol, ConsoleColor color, int damage) : base(name, symbol, color) =>
            Damage = damage;

        public int Damage { get; }

        public static Weapon Dagger() => new Weapon("Dagger", "d", ConsoleColor.Blue, damage: 15);
        public static Weapon Sword()  => new Weapon("Sword",  "s", ConsoleColor.Blue, damage: 25);
    }
}