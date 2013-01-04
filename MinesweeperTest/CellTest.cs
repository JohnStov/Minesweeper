using MinesweeperLib;
using NUnit.Framework;

namespace MinesweeperTest
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
            var cell = new Cell(null, true);

            Assert.That(cell.IsBomb, Is.True);
        }

        [Test]
        public void DefaultsToInvisible()
        {
            var cell = new Cell();

            Assert.That(cell.IsVisible, Is.False);
        }

        [Test]
        public void CanSetVisible()
        {
            var cell = new Cell();
            cell.SetVisible();

            Assert.That(cell.IsVisible, Is.True);
        }
    }
}