using System;
using System.Collections.Generic;
using System.Linq;

namespace MinesweeperLib
{
    public class Board
    {
        private readonly Cell[,] cells;

        internal class Point : IEquatable<Point>
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; private set; }
            public int Y { get; private set; }

            public bool Equals(Point other)
            {
                return X == other.X && Y == other.Y;
            }
        }

        public Board(string init)
        {
            var rows = init.Split(new [] {",", "\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            var height = rows.Count();
            var width = height == 0 ? 0 : rows.Max(x => x.Length);

            cells = new Cell[width,height];
            for (int i = 0; i < height; ++i)
                for (int j = 0; j < width; ++j)
                    cells[j,i] = new Cell(this, rows[i].Length > j && rows[i][j] == '*');
        }

        public Board(int width, int height, int bombCount)
        {
            var bombList = new List<Point>();
            var rnd = new Random();

            while (bombList.Count < bombCount)
            {
                var pt = new Point(rnd.Next(width), rnd.Next(height));
                if (bombList.All(x => !x.Equals(pt)))
                    bombList.Add(pt);
            }

            cells = new Cell[width, height];
            for (int i = 0; i < height; ++i)
                for (int j = 0; j < width; ++j)
                {
                    var pt = new Point(j, i);
                    var isBomb = bombList.Any(x => x.Equals(pt));
                    cells[j, i] = new Cell(this, isBomb);
                }
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

        public bool Dig(int x, int y)
        {
            var gameOver = this[x, y].IsBomb;
            if (!gameOver)
                this[x, y].SetVisible();

            return !gameOver;
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
            int x, y;
            if (FindCellPosition(cell, out x, out y))
                return NeighbourMineCount(x, y);

            return 0;
        }

    }
}
