using System;
namespace Connect4.GameServer
{
	public class GameServer
	{
		private GameBoard board;
		public GameBoard Board  //public board to show externally, don't let anyone touch our internal one so they can't cheat!
		{
			get
			{
				return board;
			}
		}
		private bool playerSwitcher;

		public GameServer()
		{
			board = new GameBoard();

			playerSwitcher = new Random().Next(10) > 5; //randomise who starts as there is a way to play perfectly that any player who starts can win if they choose the middle column

			GameHasWinner = false;
		}

      
		private bool GameHasWinner;

		public bool GameHasBeenWon()
		{
			return GameHasWinner;
		}


		public PositionState CurrentPlayer()
		{
			if (playerSwitcher)
			{
				return PositionState.Yellow;
			}

			return PositionState.Red;
		}



		public bool TakeTurn(char turn)
		{
			int column;
			//is turn a valid column
			try
			{

				column = Convert.ToInt32(turn.ToString()) - 1;//cast it to anint to make sure we're dealing with a number

				if (!IsAValidColumn(column))
				{
					throw new Exception();
				}
			}
			catch (Exception)
			{
				Console.WriteLine("Please enter a valid column to place the chip in from 1 to 7");
				return false;
			}

			return TakeTurn(column);

		}

        void TakeComputerTurn()
        {
            //computer always yellow, check whether computer is first to play
            if (CurrentPlayer() == PositionState.Yellow)
            {
                var rand = new Random();

                TakeTurn(rand.Next(6));
            }
        }

		public bool TakeTurn(int column)
		{
			//check turn is valid
			var move = CanInsertIntoThisColumn(column, CurrentPlayer());
			if (!move.Success)
			{
				Console.WriteLine("there is no room in this column, please choose another.");
                if (CurrentPlayer() == PositionState.Yellow)    //added this as if column was full found player would be asked to enter for computers turn
                    TakeComputerTurn();


                return false;
			}

			GameHasWinner = board.IsThisMovePartOfAWinningRow(move);

			if (!GameHasWinner)
			{
				playerSwitcher = !playerSwitcher;//no winner so switch player
				TakeComputerTurn();
			}

			return true;
		}
		private Move CanInsertIntoThisColumn(int column, PositionState player)
		{
			return board.CheckIfThereIsSpaceThenInsertChip(column, player);
		}

		private bool IsAValidColumn(int column)
		{
			return column >= 0 && column < 7;  //check is between 0 and 6
		}

		public void Initialise()
		{
			board.SetupBoard();

			TakeComputerTurn();

		}




        public bool BoardHasSpace()
        {
            return board.IsThereAnyRoom();   
        }
    }
}
