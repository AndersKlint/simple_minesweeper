using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    /// <summary>
    /// A node object to be used inside a Minefield.
    /// </summary>
    class FieldNode
    {
        private bool isUncovered = false;
        private bool isBombs = false;
        private int adjacentBombs = 0;

        public bool IsUncovered
        {
            get { return isUncovered; }
            set { isUncovered = value; }
        }
        public bool IsBomb
        {
            get { return isBombs; }
            set { isBombs = value; }
        }

        public int AdjacentBombs
        {
            get { return adjacentBombs; }
            set { adjacentBombs = value; }
        }


        /// <summary>
        /// Prints the correct representation of the node depending on it's state.
        /// </summary>
        public void PrintNode()
        {
            if (isUncovered)
            {
                if (isBombs)
                {
                    Console.Write('X');
                }
                else if (adjacentBombs > 0)
                {
                    Console.Write(adjacentBombs);
                }
                else
                {
                    Console.Write(" ");
                }
            }
            else
            {
                Console.Write('?');
            }
        }
    }
}
