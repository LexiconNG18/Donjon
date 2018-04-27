using System;
using Donjon.Entities.Creatures;
using Donjon.Utils;

namespace Donjon.Entities.Items {
    abstract class Potion : Item, IConsumable {
        protected int UsesLeft = 1;

        protected Potion(string name, string symbol, ConsoleColor color) : base(name, symbol, color) { }

        public virtual Action<Log> Affect(Hero hero) {
            if (UsesLeft <= 0) return log => log.Add($"There is no {Name} left");
            UsesLeft--;
            if (UsesLeft <= 0) hero.Backpack.Remove(this);
            return ApplyEffect(hero);
        }

        protected abstract Action<Log> ApplyEffect(Hero hero);
    }
}