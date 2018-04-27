using System;
using System.Linq;
using Donjon.Entities.Items;
using Donjon.Utils;

namespace Donjon.Entities.Creatures {
    class Hero : Creature {
        public Hero() : base("Hero", "H", ConsoleColor.White, health: 50) => BaseDamage = 10;

        public override int          Damage => BaseDamage + (Wielded?.Damage ?? 0);
        public override ConsoleColor Color  => IsDead ? ConsoleColor.Gray : base.Color;

        public Weapon            Wielded  { get; set; }
        public LimitedList<Item> Backpack { get; } = new LimitedList<Item>(capacity: 3);

        public override bool Walk(Position movement) {
            if (base.Walk(movement)) {
                var items = Cell.Items;
                switch (items.Count) {
                case 0: break;
                case 1:
                    Log.Add($"You see a {items[index: 0].Name} here");
                    break;
                default:
                    Log.Add("There are some things here; " +
                            items.Select(i => i.Name)
                                 .JoinedBy(", "));
                    break;
                }

                return true;
            }

            var target = Position + movement.Step;
            if (Level.CreatureAt(target) is Monster monster) return Attack(monster);

            return false;
        }

        public bool PickUp(Item item) {
            if (Backpack.IsFull) {
                Log.Add("Alas, your backpack is full. Try dropping something");
                return false;
            }

            var floor = Cell.Items;

            if (!floor.Remove(item)) {
                Log.Add($"Your couldn't pick upp the {item.Name}");
                return false;
            }

            if (item is Weapon weapon && Wielded == null) {
                Wielded = weapon;
                Log.Add($"You pick up and wield the {item.Name}");
                return true;
            }

            Backpack.Add(item);
            Log.Add($"You pick up the {item.Name}");
            return true;
        }

        public bool Consume(IConsumable potion) {
            var callback = potion.Affect(this);
            if (callback == null) return false;
            callback(Log);
            return true;
        }
    }
}