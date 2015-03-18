using System;
using System.Text;
using Connect4.GameServer;

namespace Connect4.UI
{
    class Program
    {

        static GameServer.GameServer gameserver = new GameServer.GameServer();

        static void Main(string[] args)
        {
            gameserver.Initialise();
            while (!gameserver.GameHasBeenWon() && gameserver.BoardHasSpace())
            {

                ShowGameBoard();

                TakeTurn();

                Console.Clear();
            }

            ShowGameBoard();
            if (gameserver.GameHasBeenWon())
            {
                Console.WriteLine("********************************************************************");
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Player: " + gameserver.CurrentPlayer() + " Winner!!!");
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("********************************************************************");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("********************************************************************");
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(" Game is a draw!!!");
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("********************************************************************");
                Console.ReadKey();

            }


        }

        private static void ShowGameBoard()
        {
            string space = "  ";
            Console.WriteLine("Connect4");
            Console.WriteLine("********************************************************************");
            Console.WriteLine(Environment.NewLine);


            for (int i = gameserver.Board.Board.GetUpperBound(1); i >= 0; i--)
            {
                for (int j = 0; j <= gameserver.Board.Board.GetUpperBound(0); j++)
                {
                    var letter = gameserver.Board.Board[j, i].State.ToString().Substring(0, 1);
                    switch (letter)
                    {
                        case "R":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(letter + space);
                            Console.ResetColor();
                            break;
                        case "Y":
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(letter + space);
                            Console.ResetColor();
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write(letter + space);
                            Console.ResetColor();
                            break;
                    }

                }
                Console.Write(Environment.NewLine);
            }

            //for (int i = gameserver.Board.Board.GetUpperBound(1); i >= 0; i--)
            //{
            //    StringBuilder row = new StringBuilder();
            //    for (int j = 0; j <= gameserver.Board.Board.GetUpperBound(0); j++)
            //    {
            //        row.Append(gameserver.Board.Board[j, i].State.ToString().Substring(0, 1));
            //        row.Append(space);
            //    }
            //    Console.WriteLine(row.ToString());
            //}
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= gameserver.Board.Board.GetUpperBound(0); i++)
            {
                sb.Append(i + 1);
                sb.Append(space);
            }
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine(sb.ToString());


        }

        private static void TakeTurn()
        {
            bool validTurn = false;

            Console.WriteLine("Choose a column to place your next disc " + gameserver.CurrentPlayer());

            while (!validTurn)
            {
                var turn = Console.ReadKey().KeyChar;
                validTurn = gameserver.TakeTurn(turn);
            }
        }
    }
}
