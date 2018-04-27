using System;
using Donjon.Entities.Creatures;
using Donjon.Utils;

namespace Donjon.Entities.Items {
    interface IConsumable : IDrawable {
        Action<Log> Affect(Hero hero);
    }
}