using Donjon.Utilities;

namespace Donjon.Entities
{
    internal class Hero : Creature
    {
        public LimitedList<Item> Backpack { get; } = new LimitedList<Item>(2);

        public Hero() : base(
            name: "Hero",
            symbol: "H",
            color: System.ConsoleColor.White)
        {
            Damage = 10;
        }
    }
}