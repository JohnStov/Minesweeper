using System;

namespace MinesweeperLib
{
    public class Cell
    {
        private readonly WeakReference boardRef;

        public Cell(Board board = null, bool isBomb = false)
        {
            boardRef = new WeakReference(board);
            IsBomb = isBomb;
            IsVisible = false;
        }

        public bool IsBomb { get; private set; } 

        private Board Board { get {return (Board)boardRef.Target;} }

        public int MineCount { get { return Board.MineCount(this); } }

        public void SetVisible()
        {
            IsVisible = true;
        }
        
        public bool IsVisible { get; private set; }
    }
}