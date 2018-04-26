using System;

namespace Donjon.Entities
{
    internal interface IDrawable
    {
        string Name { get; }
        string Symbol { get; }
        ConsoleColor Color { get; }
    }
}
