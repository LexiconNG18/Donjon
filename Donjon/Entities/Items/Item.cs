using System;

namespace Donjon.Entities.Items {
    class Item : IDrawable {
        protected Item(string name, string symbol, ConsoleColor color) {
            Color  = color;
            Name   = name;
            Symbol = symbol;
        }

        public static Item Heap => new Item("Heap", "#", ConsoleColor.Gray);
        public static Item Coin => new Item("Coin", "c", ConsoleColor.Yellow);
        public static Item Gem  => new Item("Gem",  "g", ConsoleColor.Magenta);

        public string       Name   { get; }
        public string       Symbol { get; }
        public ConsoleColor Color  { get; }
    }
}