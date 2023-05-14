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
            Board.placePiece (new Knight (Board, Color.White), new ChessPosition ('e', 4).toPosition());
        }
    }
}
