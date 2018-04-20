using System;

namespace Donjon.Entities
{
    class Goblin : Monster
    {
        public Goblin() : base("Goblin", "G", ConsoleColor.Green)
        {
            Damage = 7;
        }
    }
}
