using System.Collections.Generic;
using System.Linq;
using Donjon.Entities;
using Donjon.Entities.Creatures;
using Donjon.Entities.Items;
using Donjon.Utils;

namespace Donjon
{
    internal class Level
    {
        private readonly Log log;
        private readonly Map map;

        public Level(Map map, Log log)
        {
            this.map = map;
            this.log = log;
        }

        private IEnumerable<Cell> Cells => map.Cells;
        private IEnumerable<Cell> PopulatedCells => Cells.Where(c => c.Creature != null);
        private IEnumerable<Creature> Creatures => PopulatedCells.Select(c => c.Creature);

        public IEnumerable<Monster> Monsters => Creatures.OfType<Monster>().ToList();
        public Hero Hero => Creatures.OfType<Hero>().Single();

        public void Cleanup()
        {
            foreach (var corpse in Monsters.Where(m => m.IsDead)) map.Cell(corpse.Position).Creature = null;
        }

        public Cell Cell(Position position)
        {
            return Cell(position.X, position.Y);
        }

        public Cell Cell(int x, int y)
        {
            return map.Cell(x, y);
        }

        public bool PlaceAt(int x, int y, Item item)
        {
            var cell = Cell(x, y);
            if (cell == null) return false;
            cell.Items.Add(item);
            return true;
        }

        public bool PlaceAt(int x, int y, Creature creature)
        {
            var position = new Position(x, y);
            if (IsBlockedAt(position)) return false;

            creature.Log = log;
            creature.Level = this;
            creature.Position = position;
            Cell(position).Creature = creature;

            return true;
        }

        public bool Move(Position originPosition, Position destinationPosition)
        {
            var destination = Cell(destinationPosition);

            var creature = CreatureAt(originPosition);
            if (creature == null) return false;
            if (IsBlockedAt(destinationPosition)) return false;

            creature.Position = destinationPosition;
            
            var origin = Cell(originPosition);
            destination.Creature = origin.Creature;
            origin.Creature = null;
            
            if (destination.Environment == Environment.Abyss)
            {
                creature.Health = 0;
                log.Add($"The {creature.Name} steps into the Abyss and disappears");
            }

            return true;
        }

        public Creature CreatureAt(Position position)
        {
            return Cell(position)?.Creature;
        }

        public bool IsBlockedAt(Position position)
        {
            var cell = Cell(position);
            return cell == null || cell.Creature != null || cell.Environment.IsBlocking;
        }
    }
}