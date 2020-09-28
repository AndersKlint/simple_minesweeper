using System;

namespace Minesweeper
{
    public class Util
    {
        /// <summary>
        /// Reads the input until a correct sequence of "X Y" is given, where X an Y are integers.
        /// </summary>
        /// <param name="fieldSize">The size of the mineField.</param>
        /// <returns>The input coordiantes as a tuple of ints if the correct input was given, otherwise null.</returns>
        /// <exception cref="ArgumentException">
        /// If <param name="input"> is not given on the correct form.
        /// </exception>
        public static Tuple<int, int> ParseInput(int fieldSize, string input)
        {
            if (input.Trim().Length == 3 && input[1] == ' ')
            {
                int x = input[0] - '0';
                int y = input[2] - '0';
                if (IsInBounds(x, y, fieldSize))
                {
                    return Tuple.Create(x, y);
                }

            }
            throw new ArgumentException();
        }

        /// <summary>
        /// Checks if the given coordinate exists within the bounds given by fieldSize.
        /// </summary>
        /// <param name="x">x-coordinate of the selected vertex.</param>
        /// <param name="y">y-coordinate of the selected vertex.</param>
        /// <param name="fieldSize">The size of the n x n field, fieldSize being n.</param>
        /// <returns>True if <param name="x"> and <param name="y"> are both between 0 and fieldSize.</returns>
        public static bool IsInBounds(int x, int y, int fieldSize)
        {
            return (x < fieldSize && y < fieldSize && x >= 0 && y >= 0);
        }
    }
}
