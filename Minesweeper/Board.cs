using System;

namespace Minesweeper
{
    using System.Linq;

    public class Board
    {
        private readonly Cell[,] cells;

        public Board(string init)
        {
            init = init.Replace("\r", "");
            int width = 0;
            int height = 0;
            string[] rows = new string[0];


            if (!string.IsNullOrEmpty(init))
            {
                rows = init.Split(',', '\n').Where(row => !string.IsNullOrEmpty(row)).ToArray();
                height = rows.Count();
                width = rows.Max(x => x.Length);
            }

            cells = new Cell[width,height];
            for (int i = 0; i < height; ++i)
                for (int j = 0; j < width; ++j)
                    cells[j,i] = new Cell(this, rows[i].Length > j && rows[i][j] == '*');
        }

        public int Width { get { return cells.GetLength(0); } }
        public int Height { get { return cells.GetLength(1); } }

        public Cell this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Width)
                    throw new ArgumentOutOfRangeException("x");
                if (y < 0 || y >= Height)
                    throw new ArgumentOutOfRangeException("y");

                return cells[x, y];
            }
        }

        private bool FindCellPosition(Cell cell, out int x, out int y)
        {
            for (y = 0; y < Height; ++y)
                for (x = 0; x < Width; ++x)
                    if (cells[x, y] == cell)
                        return true;

            x = y = -1;
            return false;
        }

        private int NeighbourMineCount(int x, int y)
        {
            int count = 0;

            for (int j = y - 1; j <= y + 1; ++j)
                for (int i = x - 1; i <= x + 1; ++i)
                    if (i >= 0 && i < Width && j >= 0 && j < Height && cells[i, j].IsBomb)
                        ++count;

            return count;
        }

        internal int MineCount(Cell cell)
        {
            int x;
            int y;

            if (FindCellPosition(cell, out x, out y))
                return NeighbourMineCount(x, y);

            return 0;
        }
    }
}
