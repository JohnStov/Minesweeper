using Minesweeper;

using NUnit.Framework;

namespace MinsweeperTest
{
    [TestFixture]
    public class CellTest
    {
        [Test]
        public void CanCreate()
        {
            var cell = new Cell();

            Assert.That(cell, Is.Not.Null);
        }

        [Test]
        public void CanCreateAsNotBomb()
        {
            var cell = new Cell();

            Assert.That(cell.IsBomb, Is.False);
        }

        [Test]
        public void CanCreateAsBomb()
        {
            var cell = new Cell(true);

            Assert.That(cell.IsBomb, Is.True);
        }
    }
}