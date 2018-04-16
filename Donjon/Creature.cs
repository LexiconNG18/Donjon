using System;

namespace Donjon
{
    internal abstract class Creature
    {
        public string Name { get; set; }
        public ConsoleColor Color { get; set; }
        public string Symbol { get; set; }
        public Position Position { get; set; }

        protected Creature(string name, string symbol, ConsoleColor color)
        {
            Name = name;
            Symbol = symbol;
            Color = color;
        }

    }
}