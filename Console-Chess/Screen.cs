using System.Collections.Generic;
using System;
using board;
using chess;

namespace Console_Chess
{
    internal class Screen
    {
        public static void printGame(ChessGame game)
        {
            Console.Clear ();
            Screen.printBoard (game.Board);

            printCapturedPieces (game);

            Console.WriteLine("\nTurn: " + game.Turn);

            if (!game.Ended)
            {
                System.Console.WriteLine("Waiting for a move: " + game.CurrentPlayer);

                if (game.Check)
                {
                    Console.WriteLine("CHECK");
                }
            }
            else
            {
                Console.WriteLine("CHECKMATE");
                Console.WriteLine("Winner: " + game.CurrentPlayer);
            }
        }

        public static void printCapturedPieces (ChessGame game)
        {
            Console.WriteLine("Captured Pieces: ");
            Console.Write("White: ");
            printHash(game.capturedPieces(Color.Black));

            Console.Write("\nBlack: ");
            printHash(game.capturedPieces(Color.White));
        }

        public static void printHash (HashSet<Piece> hash)
        {
            Console.Write("[ ");
            foreach (Piece p in hash)
            {
                printPiece(p);
            } 
            Console.Write("]");
        }

        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    printPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h\n");
        }

        public static void printBoard(Board board, bool[,] mat)
        {
            ConsoleColor orignial = Console.BackgroundColor;
            ConsoleColor altered = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        Console.BackgroundColor = altered;
                    }
                    else
                    {
                        Console.BackgroundColor = orignial;
                    }
                    printPiece(board.Piece(i, j));
                    Console.BackgroundColor = orignial;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h\n");        }

        public static ChessPosition readChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }

        public static void printPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece + " ");
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece + " ");
                    Console.ForegroundColor = aux;
                }
            }
        }
    }
}
