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
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;

        public ChessGame ()
        {
            Board = new Board (8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Ended = false; 
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            PlacePieces ();
        }

        private void executeMove(Position origin, Position destination)
        {
            Piece p = Board.takePiece(origin);
            p.addMove ();
            Piece takenPiece = Board.takePiece(destination);
            Board.placePiece (p, destination);
            if (takenPiece != null)
            {
                Pieces.Remove (takenPiece);
                Captured.Add (takenPiece);
            }
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

        public HashSet<Piece> capturedPieces (Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in Captured)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }

            return aux;
        }

        public HashSet<Piece> livePieces (Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in Pieces)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }


            return aux;
        }

        public void placeNewPiece (char column, int line, Piece piece)
        {
            Board.placePiece (piece, new ChessPosition (column, line).toPosition());
            Pieces.Add (piece);
        }

        private void PlacePieces ()
        {
            placeNewPiece ('d', 1, new King (Board, Color.White));
            placeNewPiece ('d', 8, new King (Board, Color.Black));
            placeNewPiece ('e', 1, new Rook (Board, Color.White));
            placeNewPiece ('e', 8, new Knight (Board, Color.Black));
        }
    }
}
