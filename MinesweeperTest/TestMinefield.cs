using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace MinesweeperTest
{
    [TestClass]
    public class TestMinefield
    {
        private const int DEFAULT_FIELD_SIZE = 4;
        private const int DEFAULT_BOMBS = 2;
        [TestMethod]
        public void AllNonBombsUncovered_NoneUncovered_ReturnsFalse()
        {
            Minefield minefield = new Minefield(DEFAULT_FIELD_SIZE);
            minefield.AddBombs(DEFAULT_BOMBS);
            Assert.IsFalse(minefield.AllNonBombsUncovered());
        }

        [TestMethod]
        public void AllNonBombsUncovered_NoneUncoveredNoBombs_ReturnsFalse()
        {
            Minefield minefield = new Minefield(DEFAULT_FIELD_SIZE);

            Assert.IsFalse(minefield.AllNonBombsUncovered());
        }

        [TestMethod]
        public void AllNonBombsUncovered_OnlyBombsInField_ReturnsTrue()
        {
            Minefield minefield = new Minefield(DEFAULT_FIELD_SIZE);
            minefield.AddBombs(DEFAULT_FIELD_SIZE * DEFAULT_FIELD_SIZE); // Filling the field with bombs.

            Assert.IsTrue(minefield.AllNonBombsUncovered());
        }

        [TestMethod]
        public void AllNonBombsUncovered_AllUncoveredNoBombs_ReturnsTrue()
        {
            Minefield minefield = new Minefield(DEFAULT_FIELD_SIZE);
            minefield.UncoverNode(0, 0);

            Assert.IsTrue(minefield.AllNonBombsUncovered());
        }


        [TestMethod]
        public void UncoverNode_BombUncovered_ReturnsTrue()
        {
            Minefield minefield = new Minefield(DEFAULT_FIELD_SIZE);
            minefield.AddBombs(DEFAULT_FIELD_SIZE * DEFAULT_FIELD_SIZE); // Filling the field with bombs.

            Assert.IsTrue(minefield.UncoverNode(0, 0));
        }

        [TestMethod]
        public void UncoverNode_NonBombUncovered_ReturnsFalse()
        {
            Minefield minefield = new Minefield(DEFAULT_FIELD_SIZE);

            Assert.IsFalse(minefield.UncoverNode(0, 0));
        }

        [TestMethod]
        public void AddBombs_XBombsAdded_XBombsInField()
        {
            Minefield minefield = new Minefield(DEFAULT_FIELD_SIZE);
            minefield.AddBombs(DEFAULT_BOMBS);
            int bombs = 0;
            for (int i = 0; i < DEFAULT_FIELD_SIZE; i++)
            {
                for (int j = 0; j < DEFAULT_FIELD_SIZE; j++)
                {
                    bombs += minefield.UncoverNode(i, j) ? 1 : 0;
                }
            }

            Assert.AreEqual(DEFAULT_BOMBS, bombs);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void AddBombs_MoreBombsThanNodesAdded_ThrowsException()
        {
            Minefield minefield = new Minefield(DEFAULT_FIELD_SIZE);
            minefield.AddBombs(DEFAULT_FIELD_SIZE * DEFAULT_FIELD_SIZE + 1);
        }

    }
}
