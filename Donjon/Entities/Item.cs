using System;

namespace Donjon.Entities
{
    class Item : IDrawable
    {
        public string Name { get; }
        public string Symbol { get; }
        public virtual ConsoleColor Color { get; }

        protected Item(string name, string symbol, ConsoleColor color)
        {
            Name = name;
            Symbol = symbol;
            Color = color;
        }

        public static Item Coin() => new Item("Coin", "c", ConsoleColor.Yellow);
    }
}
