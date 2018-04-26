using System;
using Donjon.Entities.Creatures;
using Donjon.Utils;

namespace Donjon.Entities.Items
{
    internal interface IConsumable : IDrawable
    {
        Action<Log> Affect(Hero hero);
    }
}