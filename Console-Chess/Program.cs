using System;
using board;

namespace Console_Chess
{
    internal class Program
    {
        static void Main (string [] args)
        {
            Position P = new Position (3, 4);

            Console.WriteLine ("Position: " + P);

            Board board = new Board (8, 8);
            Console.WriteLine ("Board generated successfully\n");

            Screen.printBoard (board);
        }
    }
}