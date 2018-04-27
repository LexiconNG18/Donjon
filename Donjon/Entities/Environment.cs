using System;

namespace Donjon.Entities {
    class Environment : IDrawable {
        private Environment(string name, string symbol, ConsoleColor color, bool isBlocking = false) {
            Color      = color;
            Name       = name;
            Symbol     = symbol;
            IsBlocking = isBlocking;
        }

        public ConsoleColor Background { get; set; } = ConsoleColor.Black;
        public bool         IsBlocking { get; }

        public static Environment Floor { get; } = new Environment("Floor", ".", ConsoleColor.DarkGray);
        public static Environment Abyss { get; } = new Environment("Abyss", ".", ConsoleColor.Black);

        public static Environment Wall { get; } = new Environment("Wall", "█", ConsoleColor.Gray, isBlocking: true) {
            Background = ConsoleColor.Gray
        };

        public static Environment Water { get; } = new Environment("Water", " ", ConsoleColor.Blue) {
            Background = ConsoleColor.Blue
        };

        public string       Name   { get; }
        public string       Symbol { get; }
        public ConsoleColor Color  { get; }
    }
}