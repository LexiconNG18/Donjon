namespace Donjon
{
    internal class Cell
    {
        public Position Position { get; }
        public string Symbol
        {
            get {
                if (Creature != null)
                {
                    return Creature.Symbol;
                }
                else
                {
                    return ".";
                }
            }
        }

        public Creature Creature { get; private set; }

        public Cell(Position position)
        {
            Position = position;
        }
    }
}