using System;

namespace Donjon
{
    internal struct Position
    {
        public readonly int X;
        public readonly int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Position operator +(Position p1, Position p2)
        {
            return new Position(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Position operator -(Position p1, Position p2)
        {
            return p1 + -p2;
        }

        public static Position operator -(Position p)
        {
            return new Position(-p.X, -p.Y);
        }

        public static bool operator ==(Position p1, Position p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Position p1, Position p2)
        {
            return !p1.Equals(p2);
        }

        public override bool Equals(object obj)
        {
            return obj is Position p && Equals(p);
        }

        public bool Equals(Position p)
        {
            return X == p.X && Y == p.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public int WalkingDistance => Math.Abs(X) + Math.Abs(Y);

        public Position Step
            => Math.Abs(X) > Math.Abs(Y)
                ? new Position(Math.Sign(X), y: 0)
                : new Position(x: 0, y: Math.Sign(Y));

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }
    }
}