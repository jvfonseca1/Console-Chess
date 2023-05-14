﻿using System;
using board;
using chess;

namespace Console_Chess
{
    internal class Screen
    {
        public static void printBoard (Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write (8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.Piece (i, j) == null)
                    {
                        Console.Write ("- ");
                    }
                    else
                    {
                        printPiece(board.Piece(i, j));
                    }
                }
                Console.WriteLine ();
            }
            Console.WriteLine ("  a b c d e f g h");
        }

        public static ChessPosition readChessPosition ()
        {
            string s = Console.ReadLine ();
            char column = s [0];
            int line = int.Parse(s [1] + "");
            return new ChessPosition(column, line);
        }

        public static void printPiece (Piece piece)
        {
            if (piece.Color == Color.White)
            {
                Console.Write (piece + " ");
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write (piece + " ");
                Console.ForegroundColor = aux;
            }
        }
    }
}
