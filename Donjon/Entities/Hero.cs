namespace Donjon.Entities
{
    internal class Hero : Creature
    {
        public Hero() : base(
            name: "Hero",
            symbol: "H",
            color: System.ConsoleColor.White)
        {
            Damage = 10;
        }
    }
}