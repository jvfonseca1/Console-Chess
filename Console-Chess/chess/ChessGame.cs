using System;
using board;

namespace chess
{
    internal class ChessGame
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Ended { get; private set; }

        public ChessGame ()
        {
            Board = new Board (8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Ended = false; 
            PlacePieces ();
        }

        private void executeMove(Position origin, Position destination)
        {
            Piece p = Board.takePiece(origin);
            p.addMove ();
            Piece takenPiece = Board.takePiece(destination);
            Board.placePiece (p, destination);
        }

        public void makePlay(Position origin, Position destination)
        {
            executeMove(origin, destination);
            Turn++;
            changePlayer();
        }

        public void validateOrigin (Position pos)
        {
            if (Board.Piece(pos) == null)
            {
                throw new BoardException("No Piece at origin position!");
            }
            else if (CurrentPlayer != Board.Piece(pos).Color)
            {
                throw new BoardException ("Chosen Piece is not the correct color!");
            } else if (!Board.Piece(pos).hasMoves())
            {
                throw new BoardException("No moves for chosen piece!");
            }
        }

        public void validateDestination (Position origin, Position destination)
        {
            if (!Board.Piece(origin).canMoveTo(destination))
            {
                throw new BoardException ("Destination not valid!");
            }
        }

        private void changePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else 
            {
                CurrentPlayer = Color.White;
            }
        }

        private void PlacePieces ()
        {
            Board.placePiece (new King (Board, Color.White), new ChessPosition ('d', 1).toPosition());
            Board.placePiece (new King (Board, Color.Black), new ChessPosition ('d', 8).toPosition());
            Board.placePiece (new Rook (Board, Color.White), new ChessPosition ('e', 1).toPosition());
            Board.placePiece (new Rook (Board, Color.Black), new ChessPosition ('e', 8).toPosition());
        }
    }
}
