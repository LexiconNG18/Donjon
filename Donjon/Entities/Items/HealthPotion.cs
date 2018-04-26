using System;
using Donjon.Entities.Creatures;
using Donjon.Utils;

namespace Donjon.Entities.Items
{
    internal class HealthPotion : Potion
    {
        public HealthPotion() : base("Health potion", "p", ConsoleColor.Cyan)
        {
            UsesLeft = 3;
        }

        protected override Action<Log> ApplyEffect(Hero hero)
        {
            hero.Health = hero.MaxHealth;
            return log => log.Add("You feel totally refreshed");
        }
    }
}