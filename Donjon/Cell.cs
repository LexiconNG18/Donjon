using Donjon.Entities;
using System;

namespace Donjon
{
    internal class Cell
    {
        public Position Position { get; }

        public string Symbol => Creature?.Symbol ?? ".";
        public ConsoleColor Color => Creature?.Color ?? ConsoleColor.DarkGray;

        public Creature Creature { get; set; }

        public Cell(Position position)
        {
            Position = position;
        }
    }
}