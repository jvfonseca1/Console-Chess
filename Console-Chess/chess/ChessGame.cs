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
        public bool Check { get; private set; }

        public ChessGame ()
        {
            Board = new Board (8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Ended = false; 
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            Check = false;
            PlacePieces ();
        }

        private Piece executeMove(Position origin, Position destination)
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

            return takenPiece;
        }

        public void undoMove (Position origin, Position destination, Piece takenPiece)
        {
            Piece p = Board.takePiece (destination);
            p.removeMove ();
            Board.placePiece (p, origin);

            if (takenPiece != null)
            {
                Board.placePiece(takenPiece, destination);

                Captured.Remove (takenPiece);
                Pieces.Add (takenPiece);
            }
        }

        public void makePlay(Position origin, Position destination)
        {
            Piece takenPiece = executeMove(origin, destination);

            if (isInCheck(CurrentPlayer))
            {
                undoMove(origin, destination, takenPiece);
                throw new BoardException("You can't put yourself in check!");
            }
            else if (isInCheck(oponentColor(CurrentPlayer)))
            {
                Check = true;
            }
            else 
            {
                Check = false;
            }

            if (testCheckMate(oponentColor(CurrentPlayer)))
            {
                Ended= true;
            }

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
            if (!Board.Piece(origin).possibleMove(destination))
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

        private Color oponentColor (Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece checkedKing (Color color)
        {
            foreach (Piece p in livePieces(color))
            {
                if (p is King)
                {
                    return p;
                }
            }

            return null;
        }

        public bool isInCheck (Color color)
         {
            Piece king = checkedKing(color);
            
            foreach (Piece p in livePieces(oponentColor(color)))
            {
                bool [,] mat = p.possibleMoves();
                if (mat[king.Position.Line, king.Position.Column])
                {
                    return true;
                }
            }
            
            return false;
        }

        public bool testCheckMate (Color color)
        {
            if (!isInCheck(color))
            {
                return false;
            }
            
            foreach (Piece p in livePieces(color))
            {
                bool[,] mat = p.possibleMoves();

                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = p.Position;
                            Position destination = new Position(i, j);
                            Piece takenPiece = executeMove(origin, destination);
                            bool testCheck = isInCheck(color);
                            undoMove(origin, destination, takenPiece);

                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public void placeNewPiece (char column, int line, Piece piece)
        {
            Board.placePiece (piece, new ChessPosition (column, line).toPosition());
            Pieces.Add (piece);
        }

        private void PlacePieces ()
        {
            placeNewPiece ('a', 1, new King (Board, Color.White));
            placeNewPiece ('g', 8, new King (Board, Color.Black));
            placeNewPiece ('f', 1, new Rook (Board, Color.White));
            placeNewPiece('f', 2, new Rook (Board, Color.White));
        }
    }
}
