using System;

namespace Donjon.Entities.Creatures
{
    internal class Troll : Monster
    {
        public Troll() : base("Troll", "T", ConsoleColor.Green, health: 30)
        {
            BaseDamage = 15;
        }

        internal override bool Action()
        {
            return base.Action() || Regenerate();
        }

        private bool Regenerate()
        {
            if (IsDead || Health >= MaxHealth) return false;
            Health += MaxHealth / 5;
            Log.Add($"The {Name} regenerates some health");
            return true;
        }
    }
}