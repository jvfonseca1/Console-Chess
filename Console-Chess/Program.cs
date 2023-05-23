using System;
using System.Text;
using board;
using chess;

namespace Console_Chess
{
    internal class Program
    {
        static void Main (string [] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            try {
                ChessGame game = new ChessGame ();

                while (!game.Ended)
                {
                    try
                    {
                        Console.Clear ();
                        Screen.printBoard (game.Board);

                        Console.WriteLine("\nTurn: " + game.Turn);
                        System.Console.WriteLine("Waiting for a move: " + game.CurrentPlayer);

                        Console.Write ("\nOrigin: ");
                        Position origin = Screen.readChessPosition ().toPosition ();
                        game.validateOrigin(origin);

                        bool[,] possiblePositions = game.Board.Piece(origin).possibleMoves();

                        Console.Clear();
                        Screen.printBoard(game.Board, possiblePositions);


                        Console.Write ("\nDestination: ");
                        Position destination = Screen.readChessPosition ().toPosition ();
                        game.validateDestination(origin, destination);

                        game.makePlay (origin, destination);
                    }
                    catch(BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                
            }
            catch (BoardException e)
            {
                Console.WriteLine (e);
            }
            

            
            Console.ReadLine ();
        }
    }
}