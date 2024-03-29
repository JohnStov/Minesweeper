﻿using System;
using System.Linq;
using System.Text;
using MinesweeperLib;
using NUnit.Framework;

namespace MinesweeperTest
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

        [Test]
        public void CanCountNeighbouringMines()
        {
            var board = new Board(" *,  ");

            Assert.That(board[0, 0].MineCount, Is.EqualTo(1));
            Assert.That(board[1, 0].MineCount, Is.EqualTo(1));
        }

        [Test]
        public void CanCountNeighbouringMines2()
        {
            var builder = new StringBuilder();
            builder.AppendLine("      ");
            builder.AppendLine(" *    ");
            builder.AppendLine("      ");
            builder.AppendLine("  *   ");
            builder.AppendLine("      ");
            builder.Append(    "      ");
            var board = new Board(builder.ToString());

            Assert.That(board[0, 0].MineCount, Is.EqualTo(1));
            Assert.That(board[1, 0].MineCount, Is.EqualTo(1));
            Assert.That(board[1, 2].MineCount, Is.EqualTo(2));
            Assert.That(board[5, 5].MineCount, Is.EqualTo(0));
        }

        [Test]
        public void CanSaveBoard()
        {
            var builder = new StringBuilder();
            builder.AppendLine("      ");
            builder.AppendLine(" *    ");
            builder.AppendLine("      ");
            builder.AppendLine("  *   ");
            builder.AppendLine("      ");
            builder.Append(    "      ");
            var board = new Board(builder.ToString());

            var serialized = board.Save();

            var lines = serialized.Split(new [] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            Assert.That(lines.Count(), Is.EqualTo(6));
            Assert.That(lines.All(x => x.Length == 6));
            Assert.That(string.IsNullOrWhiteSpace(lines[0]));
            Assert.That(!string.IsNullOrWhiteSpace(lines[1]));
            Assert.That(string.IsNullOrWhiteSpace(lines[2]));
            Assert.That(!string.IsNullOrWhiteSpace(lines[3]));
            Assert.That(string.IsNullOrWhiteSpace(lines[4]));
            Assert.That(string.IsNullOrWhiteSpace(lines[5]));
            Assert.That(lines[1][1], Is.EqualTo('*'));
            Assert.That(lines[3][2], Is.EqualTo('*'));
        }

        [Test]
        public void CanCreateRandomBoard()
        {
            var board = new Board(10, 10, 5);
            var serialized = board.Save();

            var lines = serialized.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Assert.That(lines.Count(), Is.EqualTo(10));
            Assert.That(lines.All(x => x.Length == 10));

            int starCount = 0;
            for (int i = 0; i < serialized.Length; ++i)
                if (serialized[i] == '*')
                    ++starCount;

            Assert.That(starCount, Is.EqualTo(5));
        }
    }
}
