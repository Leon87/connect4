using System.Collections.Generic;
using System.Linq;

namespace Connect4.GameServer
{
    public class GameBoard
    {
        public GameBoard()
        {
            Board = new BoardPosition[BOARD_WIDTH, BOARD_HEIGHT];
        }

        const int BOARD_WIDTH = 7;  //game board will always be 7 x 6 
        const int BOARD_HEIGHT = 6;

        public BoardPosition[,] Board;


        public int BoardWidthForIndex
        {
            get { return BOARD_WIDTH - 1; } // - 1 due to wanting answer from player being 1 - 7 rather than 0-6
        }

        public int BoardHeightForIndex
        {
            get { return BOARD_HEIGHT - 1; }
        }



        internal Move CheckIfThereIsSpaceThenInsertChip(int column, PositionState player)
        {

            for (int i = 0; i <= Board.GetUpperBound(1); i++)
            {
                if (Board[column, i].State == PositionState.Empty)
                {
                    Board[column, i].State = player;
                    return new Move { Column = column, Row = i, Success = true };
                }
            }

            return new Move { Success = false };
        }

        internal void SetupBoard()
        {
            for (int i = 0; i <= BoardHeightForIndex; i++)
            {
                for (int j = 0; j <= BoardWidthForIndex; j++)
                {
                    Board[j, i] = new BoardPosition();
                }
            }
        }

        private bool CheckIfChipIsStartOfwinningCombination(int column, int row)
        {
            bool left = false;
            bool right = false;
            bool up = false;
            bool down = false;
            List<bool> combinationOutcomes = new List<bool>();


            if (column == 3)
            {
                left = right = true;
            }
            else if (column > 3)
            {
                left = true;
            }
            else
            {
                right = true;
            }

            if (row > 2)
            {
                down = true;
            }
            else
            {
                up = true;
            }

            if (left)
            {
                combinationOutcomes.Add(CheckLeft(column, row));

                if (up)
                {
                    combinationOutcomes.Add(CheckUp(column, row));
                    combinationOutcomes.Add(CheckUpLeft(column, row));
                }
                else
                {
                    combinationOutcomes.Add(CheckDownLeft(column, row));
                }
            }

            if (right)
            {
                combinationOutcomes.Add(CheckRight(column, row));
                if (up)
                {
                    combinationOutcomes.Add(CheckUp(column, row));
                    combinationOutcomes.Add(CheckUpRight(column, row));
                }
                else
                {
                    combinationOutcomes.Add(CheckDownRight(column, row));
                }
            }

            return combinationOutcomes.Any(obj => obj == true);
        }

        private bool CheckUp(int column, int row)
        {
            return Board[column, row].State == Board[column, row + 1].State &&
        Board[column, row + 2].State == Board[column, row + 3].State &&
        Board[column, row].State == Board[column, row + 3].State
        && Board[column, row].State != PositionState.Empty;
        }

        //these need sorting as they'r elooking at the wrong thing

        private bool CheckUpRight(int column, int row)
        {

            return Board[column, row].State == Board[column + 1, row + 1].State &&
            Board[column + 2, row + 2].State == Board[column + 3, row + 3].State &&
            Board[column, row].State == Board[column + 3, row + 3].State
            && Board[column, row].State != PositionState.Empty;
        }

        private bool CheckDownRight(int column, int row)
        {
            return Board[column, row].State == Board[column + 1, row - 1].State &&
            Board[column + 2, row - 2].State == Board[column + 3, row - 3].State &&
            Board[column, row].State == Board[column + 3, row - 3].State
            && Board[column, row].State != PositionState.Empty;
        }

        private bool CheckDownLeft(int column, int row)
        {
            return Board[column, row].State == Board[column - 1, row - 1].State &&
            Board[column - 2, row - 2].State == Board[column - 3, row - 3].State &&
            Board[column, row].State == Board[column - 3, row - 3].State
            && Board[column, row].State != PositionState.Empty;
        }

        private bool CheckUpLeft(int column, int row)
        {
            return Board[column, row].State == Board[column - 1, row + 1].State &&
            Board[column - 2, row + 2].State == Board[column - 3, row + 3].State &&
            Board[column, row].State == Board[column - 3, row + 3].State
            && Board[column, row].State != PositionState.Empty;
        }

        private bool CheckRight(int column, int row)
        {
            return Board[column, row].State == Board[column + 1, row].State &&
            Board[column + 2, row].State == Board[column + 3, row].State &&
            Board[column, row].State == Board[column + 3, row].State
            && Board[column, row].State != PositionState.Empty;
        }

        private bool CheckLeft(int column, int row)
        {
            return (Board[column, row].State == Board[column - 1, row].State) &&
            (Board[column - 2, row].State == Board[column - 3, row].State) &&
            (Board[column, row].State == Board[column - 3, row].State)
            && (Board[column, row].State != PositionState.Empty);
        }

        internal bool IsThisMovePartOfAWinningRow(Move move)
        {
            for (int row = 0; row <= BoardHeightForIndex; row++)
            {
                for (int column = 0; column <= BoardWidthForIndex; column++)
                {

                    var winnerFound = CheckIfChipIsStartOfwinningCombination(column, row);
                    if (winnerFound)
                        return true;
                }
            }

            return false;
        }

        internal bool IsThereAnyRoom()
        {
            for (int column = 0; column <= BoardWidthForIndex; column++)
            {
                var chip = Board[column, BoardHeightForIndex];  // if space on top row its ok.
                if (chip.State == PositionState.Empty)
                    return true;
            }
            return false;
        }
    }
}
