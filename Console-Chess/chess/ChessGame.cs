using System;
using board;
using Console_Chess;

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
        public Piece PossibleEnPassant { get; private set; }
        public bool PossiblePromotion { get;  private set; }

        public ChessGame ()
        {
            Board = new Board (8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Ended = false; 
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            Check = false;
            PossibleEnPassant = null;
            PossiblePromotion = false;
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
            
            //#Special Move - Short Castles
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Line, origin.Column + 3);
                Position destinationR = new Position(origin.Line, origin.Column + 1);
                Piece R = Board.takePiece(originR);
                R.addMove ();
                Board.placePiece (R, destinationR);
            }

            //#Special Move - Long Castles
            else if (p is King && destination.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Line, origin.Column - 4);
                Position destinationR = new Position(origin.Line, origin.Column - 1);
                Piece R = Board.takePiece(originR);
                R.addMove();
                Board.placePiece(R, destinationR);
            }

            //#Special Move - En Passant
            else if (p is Pawn)
            {
                 if (destination.Column != origin.Column && takenPiece == null)
                {
                    Position posP;

                    if (p.Color == Color.White)
                    {
                        posP = new Position(destination.Line + 1, destination.Column);
                    }
                    else
                    {
                        posP = new Position(destination.Line - 1, destination.Column);
                    }

                    takenPiece = Board.takePiece(posP);
                    Pieces.Remove(takenPiece);
                    Captured.Add(takenPiece);
                }
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

            //#Special Move - Short Castles
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Line, origin.Column + 3);
                Position destinationR = new Position(origin.Line, origin.Column + 1);
                Piece R = Board.takePiece(destinationR);
                R.removeMove();
                Board.placePiece(R, originR);
            }

            //#Special Move - Long Castles
            else if (p is King && destination.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Line, origin.Column - 4);
                Position destinationR = new Position(origin.Line, origin.Column - 1);
                Piece R = Board.takePiece(destinationR);
                R.removeMove();
                Board.placePiece(R, originR);
            }

            //#Special Move - En Passant
            else if (p is Pawn)
            {
                if (destination.Column != origin.Column && takenPiece == PossibleEnPassant)
                {
                    Piece pawn = Board.takePiece(destination);
                    Position posP;

                    if (p.Color == Color.White)
                    {
                        posP = new Position (3, destination.Column);
                    }
                    else
                    {
                        posP = new Position (4, destination.Column);
                    }

                    Board.placePiece(pawn, posP);
                }
            }
        }

        public void makePlay(Position origin, Position destination)
        {
            Piece takenPiece = executeMove(origin, destination);
            Piece p = Board.Piece(destination);

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

            //#Special Move - Promotion
            if (p is Pawn)
            {
                if (p.Color == Color.White && destination.Line == 0 || p.Color == Color.Black && destination.Line == 7)
                {
                    p = Board.takePiece(destination);
                    Pieces.Remove(p);
                    PossiblePromotion = true;
                    Console.Clear();
                    Screen.printGame(this);
                    int n = int.Parse(Console.ReadLine());
                    if  (n == 1)
                    {
                        Piece promoted = new Queen(Board, p.Color);
                        Pieces.Add(promoted);
                        Board.placePiece(promoted, new Position(destination.Line, destination.Column));
                    }
                    else if (n == 2)
                    {
                        Piece promoted = new Rook(Board, p.Color);
                        Pieces.Add(promoted);
                        Board.placePiece(promoted, new Position(destination.Line, destination.Column));
                    }
                    else if (n == 3)
                    {
                        Piece promoted = new Bishop(Board, p.Color);
                        Pieces.Add(promoted);
                        Board.placePiece(promoted, new Position(destination.Line, destination.Column));
                    }
                    else if (n == 4)
                    {
                        Piece promoted = new Knight(Board, p.Color);
                        Pieces.Add(promoted);
                        Board.placePiece(promoted, new Position(destination.Line, destination.Column));
                    }
                    PossiblePromotion = false;
                    Console.Clear();
                    Screen.printGame(this);
                }
            }

            if (testCheckMate(oponentColor(CurrentPlayer)))
            {
                Ended= true;
            }
            else
            {
                Turn++;
                changePlayer();
            }


            //#Special Move - En Passant
            if (p is Pawn && destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2)
            {
                PossibleEnPassant = p;
            }
            else 
            {
                PossibleEnPassant = null;
            }
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
            placeNewPiece('a', 1, new Rook(Board, Color.White));
            placeNewPiece('b', 1, new Knight(Board, Color.White));
            placeNewPiece('c', 1, new Bishop(Board, Color.White));
            placeNewPiece('d', 1, new Queen(Board, Color.White));
            placeNewPiece('e', 1, new King(Board, Color.White, this));
            placeNewPiece('f', 1, new Bishop(Board, Color.White));
            placeNewPiece('g', 1, new Knight(Board, Color.White));
            placeNewPiece('h', 1, new Rook(Board, Color.White));
            placeNewPiece('a', 2, new Pawn(Board, Color.White, this));
            placeNewPiece('b', 2, new Pawn(Board, Color.White, this));
            placeNewPiece('c', 2, new Pawn(Board, Color.White, this));
            placeNewPiece('d', 2, new Pawn(Board, Color.White, this));
            placeNewPiece('e', 2, new Pawn(Board, Color.White, this));
            placeNewPiece('f', 2, new Pawn(Board, Color.White, this));
            placeNewPiece('g', 2, new Pawn(Board, Color.White, this));
            placeNewPiece('h', 2, new Pawn(Board, Color.White, this));

            placeNewPiece('a', 8, new Rook(Board, Color.Black));
            placeNewPiece('b', 8, new Knight(Board, Color.Black));
            placeNewPiece('c', 8, new Bishop(Board, Color.Black));
            placeNewPiece('d', 8, new Queen(Board, Color.Black));
            placeNewPiece('e', 8, new King(Board, Color.Black, this));
            placeNewPiece('f', 8, new Bishop(Board, Color.Black));
            placeNewPiece('g', 8, new Knight(Board, Color.Black));
            placeNewPiece('h', 8, new Rook(Board, Color.Black));
            placeNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            placeNewPiece('b', 7, new Pawn(Board, Color.White, this));
            placeNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            placeNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            placeNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            placeNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            placeNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            placeNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }
    }
}
