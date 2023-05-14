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
                    Console.Clear ();
                    Screen.printBoard (game.Board);

                    Console.Write ("Origin: ");
                    Position origin = Screen.readChessPosition ().toPosition ();
                    Console.Write ("Destination: ");
                    Position destination = Screen.readChessPosition ().toPosition ();

                    game.executeMove (origin, destination);
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