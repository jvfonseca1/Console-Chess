using board;

namespace chess
{
    internal class King : Piece
    {
        private ChessGame Game;

        public King(Board board, Color color, ChessGame game) : base(board, color)
        {
            Game = game;
        }

        public override string ToString()
        {
            return "\u265A";
        }

        private bool canMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != Color;
        }

        private bool testRookCatling (Position pos)
        {
            Piece p = Board.Piece (pos);
            return p != null && p is Rook && p.Color == Color && p.MoveCount == 0;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            //up
            pos.defineValues(Position.Line - 1, Position.Column);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //down
            pos.defineValues(Position.Line + 1, Position.Column);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //left
            pos.defineValues(Position.Line, Position.Column - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //right
            pos.defineValues(Position.Line, Position.Column + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //ne
            pos.defineValues(Position.Line - 1, Position.Column + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //se
            pos.defineValues(Position.Line + 1, Position.Column + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //sw
            pos.defineValues(Position.Line + 1, Position.Column - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //nw
            pos.defineValues(Position.Line - 1, Position.Column - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //#Special Move Castling
            if (MoveCount == 0 && !Game.Check)
            {
                //Short
                Position posR1 = new Position(Position.Line, Position.Column + 3);

                if (testRookCatling(posR1)) 
                {
                    Position p1 = new Position (Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);

                    if (Board.Piece(p1) == null && Board.Piece(p2) == null)
                    {
                        mat[Position.Line, Position.Column + 2] = true; 
                    }
                }

                //Long
                Position posR2 = new Position(Position.Line, Position.Column - 4);

                if (testRookCatling(posR2))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);

                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null)
                    {
                        mat[Position.Line, Position.Column - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}
