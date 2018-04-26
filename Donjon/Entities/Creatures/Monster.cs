using System;

namespace Donjon.Entities.Creatures
{
    internal abstract class Monster : Creature
    {
        protected Monster(string name, string symbol, ConsoleColor color, int health = 1) : base(name, symbol, color,
            health)
        {
        }

        public override ConsoleColor Color => IsDead
            ? ConsoleColor.Gray
            : IsAggressive
                ? ConsoleColor.Red
                : base.Color;

        public bool IsAggressive { get; set; }

        internal virtual bool Action()
        {
            if (IsDead || !IsAggressive) return false;

            var d = (Level.Hero.Position - Position).WalkingDistance;
            if (d > 1) return false;

            return Attack(Level.Hero);
        }

        protected override void Defend(Creature creature, int damage)
        {
            base.Defend(creature, damage);
            IsAggressive = true;
        }

        public override bool Walk(Position movement)
        {
            if (base.Walk(movement)) return true;
            var destination = Level.Cell(Position + movement.Step);
            if (destination.Creature == Level.Hero) return Attack(destination.Creature);
            return false;
        }
    }
}