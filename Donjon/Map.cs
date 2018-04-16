using System;

namespace Donjon
{
    internal class Map
    {
        public int Width { get; }
        public int Height { get; }
        private Cell[,] cells;

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            cells = new Cell[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    cells[x, y] = new Cell(new Position(x, y));
                }
            }
        }

        internal Cell Cell(int x, int y)
        {
            // todo: validate x and y
            return cells[x, y];
        }
    }
}