using Donjon.Entities;
using System;
using System.Collections.Generic;

namespace Donjon
{
    internal class Cell : IDrawable
    {
        public Position Position { get; }

        public string Name => "Cell";
        public string Symbol => ".";
        public ConsoleColor Color => ConsoleColor.DarkGray;

        public IDrawable Appearance => Creature ?? (Items.Count > 0 ? (IDrawable) Items[0] : this);

        public Creature Creature { get; set; }
        public List<Item> Items { get; } = new List<Item>();


        public Cell(Position position)
        {
            Position = position;
        }
    }
}