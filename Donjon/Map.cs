namespace Donjon
{
    internal class Map
    {
        private readonly int width;
        private readonly int height;
        private Cell[,] cells;

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            cells = new Cell[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var position = new Position(x, y);
                    cells[x, y] = new Cell(position);
                }
            }
        }
    }
}