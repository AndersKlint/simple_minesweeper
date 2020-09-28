namespace Minesweeper
{
    using System;

    public class Minesweeper
    {
        private const int FIELD_SIZE = 5; // Must be a value between 1 and 9.
        private const int NBR_OF_BOMBS = 4; // Must be lower than FIELD_SIZE * FIELD_SIZE.
        static void Main()
        {
            bool shouldExit = false;
            // Game loop
            while (!shouldExit)
            {
                string gameResult = StartNewGame();
                Console.WriteLine(gameResult + "\nPress any key to restart. Press ESC to exit.");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                shouldExit = keyInfo.Key == ConsoleKey.Escape;
            }

        }

        /// <summary>
        /// Runs the game until it's either lost or won.
        /// </summary>
        /// <returns>The result of the game as a string.</returns>
        private static string StartNewGame( )
        {
            Console.Clear();
            var minefield = new Minefield(FIELD_SIZE);
            minefield.AddBombs(NBR_OF_BOMBS);
            minefield.PrintField();
            bool bombSelected = false;
            bool gameInProgress = true;
            while (gameInProgress)
            {
                Console.WriteLine("Enter coordinates on the form \"X Y\": ");
                Tuple<int, int> inputRes;
                try
                {
                    inputRes = Util.ParseInput(FIELD_SIZE, Console.ReadLine());
                    bombSelected = minefield.UncoverNode(inputRes.Item1, inputRes.Item2);
                    gameInProgress = !(bombSelected || minefield.AllNonBombsUncovered());
                    Console.Clear();
                    minefield.PrintField();
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Wrong input, please try again.");
                }
            }

            return bombSelected ? "KABOOM! You hit a mine!" : "Congratulations, you won!";
        }
    }
}
