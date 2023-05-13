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

            ChessPosition pos = new ChessPosition ('a', 1);
            Console.WriteLine (pos);

            Console.WriteLine (pos.toPosition());
            
            Console.ReadLine ();
        }
    }
}