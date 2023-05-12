using System;
using Board;

namespace Console_Chess
{
    internal class Program
    {
        static void Main (string [] args)
        {
            Position P = new Position (3, 4);

            Console.WriteLine ("Position: " + P);
        }
    }
}