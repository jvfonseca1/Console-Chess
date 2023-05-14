using System;
using board;

namespace chess
{
    internal class ChessGame
    {
        public Board Board { get; private set; }
        private int Turn;
        private Color CurrentPlayer;
        public bool Ended { get; private set; }

        public ChessGame ()
        {
            Board = new Board (8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Ended = false; 
            PlacePieces ();
        }

        public void executeMove(Position origin, Position destiny)
        {
            Piece p = Board.takePiece(origin);
            p.addMove ();
            Piece takenPiece = Board.takePiece(destiny);
            Board.placePiece (p, destiny);
        }

        private void PlacePieces ()
        {
            Board.placePiece (new Rook (Board, Color.Black), new ChessPosition ('a', 8).toPosition());
            Board.placePiece (new Knight (Board, Color.Black), new ChessPosition ('b', 8).toPosition());
            Board.placePiece (new Bishop (Board, Color.Black), new ChessPosition ('c', 8).toPosition());
            Board.placePiece (new Queen (Board, Color.Black), new ChessPosition ('d', 8).toPosition());
            Board.placePiece (new King (Board, Color.Black), new ChessPosition ('e', 8).toPosition());
            Board.placePiece (new Bishop (Board, Color.Black), new ChessPosition ('f', 8).toPosition());
            Board.placePiece (new Knight (Board, Color.Black), new ChessPosition ('g', 8).toPosition());
            Board.placePiece (new Rook (Board, Color.Black), new ChessPosition ('h', 8).toPosition());
            Board.placePiece (new Pawn (Board, Color.Black), new ChessPosition ('a', 7).toPosition());
            Board.placePiece (new Pawn (Board, Color.Black), new ChessPosition ('b', 7).toPosition());
            Board.placePiece (new Pawn (Board, Color.Black), new ChessPosition ('c', 7).toPosition());
            Board.placePiece (new Pawn (Board, Color.Black), new ChessPosition ('d', 7).toPosition());
            Board.placePiece (new Pawn (Board, Color.Black), new ChessPosition ('e', 7).toPosition());
            Board.placePiece (new Pawn (Board, Color.Black), new ChessPosition ('f', 7).toPosition());
            Board.placePiece (new Pawn (Board, Color.Black), new ChessPosition ('g', 7).toPosition());
            Board.placePiece (new Pawn (Board, Color.Black), new ChessPosition ('h', 7).toPosition());
            Board.placePiece (new Rook (Board, Color.White), new ChessPosition ('a', 1).toPosition());
            Board.placePiece (new Knight (Board, Color.White), new ChessPosition ('b', 1).toPosition());
            Board.placePiece (new Bishop (Board, Color.White), new ChessPosition ('c', 1).toPosition());
            Board.placePiece(new King(Board, Color.White), new ChessPosition('e', 1).toPosition());
            Board.placePiece (new Knight (Board, Color.White), new ChessPosition ('g', 1).toPosition());
            Board.placePiece(new Rook(Board, Color.White), new ChessPosition('h', 1).toPosition());
            Board.placePiece (new Pawn (Board, Color.White), new ChessPosition ('b', 2).toPosition());
            Board.placePiece (new Pawn (Board, Color.White), new ChessPosition ('c', 2).toPosition());
            Board.placePiece (new Pawn (Board, Color.White), new ChessPosition ('d', 2).toPosition());
            Board.placePiece (new Pawn (Board, Color.White), new ChessPosition ('e', 2).toPosition());
            Board.placePiece (new Pawn (Board, Color.White), new ChessPosition ('f', 2).toPosition());
            Board.placePiece (new Pawn (Board, Color.White), new ChessPosition ('g', 2).toPosition());
            Board.placePiece (new Pawn (Board, Color.White), new ChessPosition ('h', 2).toPosition());
        }
    }
}
