using System.Text;

namespace MinesweeperLib
{
    public static class BoardExtensions
    {
        public static string Save(this Board board)
        {
            var builder = new StringBuilder();
            for (int y = 0; y < board.Height; ++y)
            {
                for (int x = 0; x < board.Width; ++x)
                    builder.Append(board[x, y].IsBomb ? '*' : ' ');

                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}