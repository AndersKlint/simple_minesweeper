using System;
using System.Collections.Generic;

namespace Minesweeper
{
    /// <summary>
    /// A minefield represented by a 2d array of FieldNodes used for playing Minesweeper. 
    /// </summary>
    public class Minefield
    {
        private int _fieldSize; // The size of the square playing field. E.g. A size of 5 means a 5x5 field.
        private FieldNode[,] _field;

        /// <summary>
        /// Creates the playing field. At this stage there are no bombs in the field, call <method name="AddBombs"> to add bombs.</method>
        /// </summary>
        /// <param name="fieldSize">The size of the square playing field. E.g. A size of 5 means a 5x5 field.</param>
        public Minefield(int fieldSize)
        {
            _fieldSize = fieldSize;
            CreateField();
        }

        /// <summary>
        /// Populates the field with new FieldNodes.
        /// </summary>
        private void CreateField()
        {
            _field = new FieldNode[_fieldSize, _fieldSize];
            for (int i = 0; i < _fieldSize; i++)
            {
                for (int j = 0; j < _fieldSize; j++)
                {
                    _field[i, j] = new FieldNode();
                }
            }
        }

        /// <summary>
        /// Adds randomly placed bombs to the Minefield and updates each fieldNode's adjacentBombs property.
        /// </summary>
        /// <param name="nbrOfBombs">Amount of bombs to place. Can't be bigger than the amount of nodes in the Minefield.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <param name="nbrOfBombs"> is bigger than the total amount of nodes in the field.
        /// </exception>
        public void AddBombs(int nbrOfBombs)
        {
            if (nbrOfBombs <= _fieldSize * _fieldSize)
            {
                Random rand = new Random();
                int bombsPlaced = 0;
                while (bombsPlaced < nbrOfBombs)
                {
                    int x = rand.Next(0, _fieldSize);
                    int y = rand.Next(0, _fieldSize);
                    if (!IsBomb(x, y))
                    {
                        SetBomb(x, y);
                        bombsPlaced++;
                    }
                }
                UpdateAdjacentBombs();
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }


        /// <summary>
        /// Return the FieldNode at the given position if exists. 
        /// </summary>
        /// <param name="x">x-coordinate of the node.</param>
        /// <param name="y">y-coordinate of the node.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the given coordiantes are out of bounds.</exception>
        /// <returns>The FieldNode at the given position.</returns>
        private FieldNode GetNode(int x, int y)
        {
            if (Util.IsInBounds(x, y, _fieldSize))
            {
                return _field[x, y];
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Marks the node at the given position as a bomb.
        /// </summary>
        /// <param name="x">x-coordinate of the node to mark.</param>
        /// <param name="y">y-coordinate of the node to mark.</param>
        private void SetBomb(int x, int y)
        {
            GetNode(x, y).IsBomb = true;
        }

        /// <summary>
        /// Checks if the node at the given position is a bomb.
        /// </summary>
        /// <param name="x">x-coordinate of the node to check.</param>
        /// <param name="y">y-coordinate of the node to check.</param>
        /// <returns>True if the node is a bomb.</returns>
        private bool IsBomb(int x, int y)
        {
            return GetNode(x, y).IsBomb;
        }

        /// <summary>
        /// Calculates and updates the AdjacentBombs property of each FieldNode in the Minefield.
        /// This method should be called after all the bombs have been placed.
        /// </summary>
        private void UpdateAdjacentBombs()
        {
            for (int i = 0; i < _fieldSize; i++)
            {
                for (int j = 0; j < _fieldSize; j++)
                {
                    int bombs = 0;
                    foreach (Tuple<int, int> adjIndex in GetAdjacentCoordinates(i, j))
                    {
                        bombs += IsBomb(adjIndex.Item1, adjIndex.Item2) ? 1 : 0;

                    }
                    GetNode(i, j).AdjacentBombs = bombs;
                }
            }

        }


        /// <summary>
        /// Recursively uncovers a node, and it's adjacent nodes until a bomb is found.
        /// </summary>
        /// <param name="x">x-coordinate of the node to uncover.</param>
        /// <param name="y">y-coordinate of the node to uncover.</param>
        /// <returns>True if the selected node is a bomb.</returns>
        public bool UncoverNode(int x, int y)
        {
            FieldNode currentNode = GetNode(x, y);
            if (!currentNode.IsUncovered)
            {
                currentNode.IsUncovered = true;
                if (currentNode.IsBomb)
                {
                    Console.WriteLine("KABOOOM!");
                    return true;
                }
                else if (currentNode.AdjacentBombs == 0)
                {
                    RevealAdjacentNodes(x, y);
                }
            }
            return false;
        }

        /// <summary>
        /// Reveals all the adjacent nodes of the selected node if possible.
        /// </summary>
        /// <param name="x">x-coordinate of the selected node.</param>
        /// <param name="y">y-coordinate of the selected node.</param>
        private void RevealAdjacentNodes(int x, int y)
        {
            foreach (Tuple<int, int> adjIndex in GetAdjacentCoordinates(x, y))
            {
                if (!GetNode(adjIndex.Item1, adjIndex.Item2).IsUncovered)
                {
                    UncoverNode(adjIndex.Item1, adjIndex.Item2);
                }
            }
        }

        /// <summary>
        /// Retreives the coordinates of the nodes adjacent to the selected node.
        /// </summary>
        /// <param name="x">x-coordinate of the selected node.</param>
        /// <param name="y">y-coordinate of the selected node.</param>
        /// <returns>A list of tuples with each adjacent node's x and y coordinates.</returns>
        private List<Tuple<int, int>> GetAdjacentCoordinates(int x, int y)
        {
            List<Tuple<int, int>> coordinateList = new List<Tuple<int, int>>();
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    int adjX = x + dx;
                    int adjY = y + dy;
                    if (!(dx == 0 && dy == 0))
                    {
                        if (Util.IsInBounds(adjX, adjY, _fieldSize))
                        {
                            coordinateList.Add(Tuple.Create(adjX, adjY));
                        }
                    }
                }
            }
            return coordinateList;
        }


        /// <summary>
        /// Returns true if the game is won, i.e all nodes are uncovered except the bomb nodes.
        /// </summary>
        /// <returns></returns>
        public bool AllNonBombsUncovered()
        {
            foreach (FieldNode node in _field)
            {
                if (!node.IsBomb)
                {
                    if (!node.IsUncovered)
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Prints the current state of the Minefield to console output.
        /// </summary>
        public void PrintField()
        {
            Console.Write("  ");
            for (int i = 0; i < _fieldSize; i++)
            {
                Console.Write(i);
            }

            for (int i = (_fieldSize - 1); i >= 0; i--)
            {
                Console.WriteLine();
                Console.Write(i + "|");
                for (int j = 0; j < _fieldSize; j++)
                {
                    GetNode(j, i).PrintNode();
                }
            }
            Console.WriteLine();
        }
    }
}
