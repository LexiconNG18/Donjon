using System;
using System.Linq;
using Donjon.Entities.Items;
using Donjon.Utils;

namespace Donjon.Entities.Creatures
{
    internal class Hero : Creature
    {
        public Hero() : base("Hero", "H", ConsoleColor.White, health: 50)
        {
            BaseDamage = 10;
        }

        public override int Damage => BaseDamage + (Weapon?.Damage ?? 0);

        public Weapon Weapon { get; set; }
        public LimitedList<Item> Backpack { get; } = new LimitedList<Item>(capacity: 3);

        public override bool Walk(Position movement)
        {
            if (base.Walk(movement))
            {
                var items = Level.Cell(Position).Items;
                switch (items.Count)
                {
                    case 0:
                        break;
                    case 1:
                        Log.Add($"You see a {items[index: 0].Name} here");
                        break;
                    default:
                        Log.Add("There are some things here; " + string.Join(", ", items.Select(i => i.Name)));
                        break;
                }

                return true;
            }

            var target = Position + movement.Step;
            var creature = Level.CreatureAt(target);
            if (creature is Monster) return Attack(creature);

            return false;
        }

        public bool PickUp(Item item)
        {
            var items = Level.Cell(Position).Items;
            if (item is Weapon weapon && Weapon == null)
            {
                var taken = items.Remove(weapon);
                if (taken) Weapon = weapon;
                Log.Add($"You pick up and wield the {item.Name}");
                return taken;
            }

            if (Backpack.IsFull())
            {
                Log.Add("Alas, your backpack is full");
                return false;
            }

            Log.Add($"You pick up the {item.Name}");
            return items.Remove(item) && Backpack.Add(item);
        }

        public bool Consume(IConsumable potion)
        {
            var callback = potion.Affect(this);
            if (callback == null) return false;
            callback(Log);
            return true;
        }
    }
}