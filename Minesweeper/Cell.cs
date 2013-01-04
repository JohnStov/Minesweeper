namespace Minesweeper
{
    public class Cell
    {
        public Cell(bool isBomb = false)
        {
            IsBomb = isBomb;
        }

        public bool IsBomb { get; private set; } 
    }
}