using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;
using System;

namespace MinesweeperTest
{
    [TestClass]
    public class TestUtil
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseInput_WrongInput_ThrowsException()
        {
            const string mockInput = "abc";
            const int fieldSize = 5;

            Util.ParseInput(fieldSize, mockInput);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseInput_InputOutOfRange_ThrowsException()
        {
            const string mockInput = "6 0";
            const int fieldSize = 5;

            Util.ParseInput(fieldSize, mockInput);
        }

        [TestMethod]
        public void ParseInput_ValidInput_ReturnsTupleOfInput()
        {
            const string mockInput = "0 4";
            const int fieldSize = 5;

            Tuple<int, int> res = Util.ParseInput(fieldSize, mockInput);
            Assert.AreEqual(0, res.Item1);
            Assert.AreEqual(4, res.Item2);
        }

        [TestMethod]
        public void IsInBounds_InBounds_ReturnsTrue()
        {
            const int fieldSize = 5;
            Assert.IsTrue(Util.IsInBounds(4, 0, fieldSize));
        }

        [TestMethod]
        public void IsInBounds_OutOfBoundsPositive_ReturnsFalse()
        {
            const int fieldSize = 5;
            Assert.IsFalse(Util.IsInBounds(5, 0, fieldSize));
        }

        [TestMethod]
        public void IsInBounds_OutOfBoundsNegative_ReturnsFalse()
        {
            const int fieldSize = 5;
            Assert.IsFalse(Util.IsInBounds(0, -1, fieldSize));
        }
    }
}
