using System.Linq;
using System.Text;

using Minesweeper;

using NUnit.Framework;

namespace MinsweeperTest
{
    [TestFixture]
    public class BoardTest
    {
        [Test]
        public void CanCreate()
        {
            var board = new Board("");

            Assert.That(board, Is.Not.Null);
        }

        [Test]
        public void HasDefaultSize()
        {
            var board = new Board("");

            Assert.That(board.Width, Is.EqualTo(0));
            Assert.That(board.Height, Is.EqualTo(0));
        }

        [Test]
        public void CanInitializeSingleRow()
        {
            var board = new Board("  ");

            Assert.That(board.Width, Is.EqualTo(2));
            Assert.That(board.Height, Is.EqualTo(1));
        }

        [Test]
        public void CanInitializeMultiRow()
        {
            var board = new Board("   ,   ");

            Assert.That(board.Width, Is.EqualTo(3));
            Assert.That(board.Height, Is.EqualTo(2));
        }

        [Test]
        public void CanInitializeMultiRowWithNewLines()
        {
            var builder = new StringBuilder();
            builder.AppendLine("  ");
            builder.AppendLine("  ");
            
            var board = new Board(builder.ToString());

            Assert.That(board.Width, Is.EqualTo(2));
            Assert.That(board.Height, Is.EqualTo(2));
        }

        [Test]
        public void CanGetCellsForBoard()
        {
            var board = new Board("  ,  ");

            Assert.That(board.Width, Is.EqualTo(2));
            Assert.That(board.Height, Is.EqualTo(2));

            foreach(var x in Enumerable.Range(0,2))
                foreach(var y in Enumerable.Range(0,2))
                    Assert.That(board[x,y], Is.Not.Null);
        }

        [Test]
        public void CanCreateBoardWithBomb()
        {
            var board = new Board(" *,  ");

            Assert.That(board[0, 0].IsBomb, Is.False);
            Assert.That(board[1, 0].IsBomb, Is.True);
        }
    }
}
