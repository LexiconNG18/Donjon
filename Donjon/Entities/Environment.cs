using System;

namespace Donjon.Entities
{
    internal class Environment : IDrawable
    {
        private Environment(string name, string symbol, ConsoleColor color, bool isBlocking = false)
        {
            Color = color;
            Name = name;
            Symbol = symbol;
            IsBlocking = isBlocking;
        }

        public string Name { get; }
        public string Symbol { get; }
        public ConsoleColor Color { get; }
        public ConsoleColor Background { get; set; } = ConsoleColor.Black;
        public bool IsBlocking { get; }

        public static Environment Floor => new Environment("Floor", ".", ConsoleColor.DarkGray);

        public static Environment Wall => new Environment("Wall", "█", ConsoleColor.Gray, isBlocking: true)
        {
            Background = ConsoleColor.Gray
        };

        public static Environment Abyss => new Environment("Abyss", ".", ConsoleColor.Black);

        public static Environment Water => new Environment("Water", " ", ConsoleColor.Blue)
        {
            Background = ConsoleColor.Blue
        };
    }
}