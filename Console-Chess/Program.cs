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
            Board board = new Board (8, 8);

            try {
                board.placePiece (new Rook (board, Color.Black), new Position (0, 0));
                board.placePiece (new Knight (board, Color.Black), new Position (0, 1));
                board.placePiece (new Bishop (board, Color.Black), new Position (0, 2));
                board.placePiece (new Queen (board, Color.Black), new Position (0, 3));
                board.placePiece (new King (board, Color.Black), new Position (0, 4));
                board.placePiece (new Bishop (board, Color.Black), new Position (0, 5));
                board.placePiece (new Knight (board, Color.Black), new Position (0, 6));
                board.placePiece (new Rook (board, Color.Black), new Position (0, 7));
                board.placePiece (new Pawn (board, Color.Black), new Position (1, 0));
                board.placePiece (new Pawn (board, Color.Black), new Position (1, 1));
                board.placePiece (new Pawn (board, Color.Black), new Position (1, 2));
                board.placePiece (new Pawn (board, Color.Black), new Position (1, 3));
                board.placePiece (new Pawn (board, Color.Black), new Position (1, 4));
                board.placePiece (new Pawn (board, Color.Black), new Position (1, 5));
                board.placePiece (new Pawn (board, Color.Black), new Position (1, 6));
                board.placePiece (new Pawn (board, Color.Black), new Position (1, 7));
                board.placePiece (new Rook (board, Color.White), new Position (7, 0));
                board.placePiece (new Knight (board, Color.White), new Position (7, 1));
                board.placePiece (new Bishop (board, Color.White), new Position (7, 2));
                board.placePiece (new Queen (board, Color.White), new Position (7, 3));
                board.placePiece (new King (board, Color.White), new Position (7, 4));
                board.placePiece (new Bishop (board, Color.White), new Position (7, 5));
                board.placePiece (new Knight (board, Color.White), new Position (7, 6));
                board.placePiece (new Rook (board, Color.White), new Position (7, 7));
                board.placePiece (new Pawn (board, Color.White), new Position (6, 0));
                board.placePiece (new Pawn (board, Color.White), new Position (6, 1));
                board.placePiece (new Pawn (board, Color.White), new Position (6, 2));
                board.placePiece (new Pawn (board, Color.White), new Position (6, 3));
                board.placePiece (new Pawn (board, Color.White), new Position (6, 4));
                board.placePiece (new Pawn (board, Color.White), new Position (6, 5));
                board.placePiece (new Pawn (board, Color.White), new Position (6, 6));
                board.placePiece (new Pawn (board, Color.White), new Position (6, 7));
                Console.WriteLine ("Board generated successfully\n");
                Screen.printBoard (board);
            }
            catch (BoardException e)
            {
                Console.WriteLine (e);
            }
            

            
            Console.ReadLine ();
        }
    }
}