using System.Collections.Generic;
using System.Linq;
using Donjon.Entities;

namespace Donjon {
    class Map {
        private readonly Cell[,] cells;

        public Map(int width, int height) {
            Width  = width;
            Height = height;
            cells  = new Cell[width, height];
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
                cells[x, y] = new Cell();
        }

        public int               Width  { get; }
        public int               Height { get; }
        public IEnumerable<Cell> Cells  => cells.Cast<Cell>();

        public Cell Cell(Position position) => Cell(position.X, position.Y);

        public Cell Cell(int x, int y) => OutOfBounds(x, y) ? null : cells[x, y];

        private bool OutOfBounds(int x, int y) => x < 0 || x >= Width || y < 0 || y >= Height;
    }
}