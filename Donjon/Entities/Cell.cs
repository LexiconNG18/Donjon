using System;
using System.Collections.Generic;
using Donjon.Entities.Creatures;
using Donjon.Entities.Items;

namespace Donjon.Entities
{
    internal class Cell : IDrawable
    {
        
        public Environment Environment { get; set; } = Environment.Floor;

        public Creature Creature { get; set; }
        public List<Item> Items { get; } = new List<Item>();

        public IDrawable Appearance => Creature
                                       ?? (Items.Count == 0
                                           ? (IDrawable) this
                                           : (Items.Count == 1
                                               ? Items[index: 0]
                                               : Item.Heap));

        public string Name => Environment.Name;
        public string Symbol => Environment.Symbol;
        public ConsoleColor Color => Environment.Color;
    }
}