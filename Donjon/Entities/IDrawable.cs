using System;

namespace Donjon.Entities {
    public interface IDrawable {
        string       Name   { get; }
        string       Symbol { get; }
        ConsoleColor Color  { get; }
    }
}