using board;

namespace chess
{
    internal class Pawn : Piece
    {
        private ChessGame Game;

        public Pawn(Board board, Color color, ChessGame game) : base(board, color)
        {
            Game = game;
        }

        public override string ToString()
        {
            return "\u265F";
        }

        private bool hasEnemy (Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p.Color != Color;
        }

        private bool free (Position pos)
        {
            
            return Board.Piece(pos) == null;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            if (Color== Color.White)
            {
                pos.defineValues(Position.Line - 1, Position.Column);
                if (Board.validPosition(pos) && free(pos))
                {
                    mat[pos.Line, pos.Column] = true; 
                }

                pos.defineValues(Position.Line - 2, Position.Column);
                if (Board.validPosition(pos) && free(pos) && MoveCount == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.defineValues(Position.Line - 1, Position.Column - 1);
                if (Board.validPosition(pos) && hasEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.defineValues(Position.Line - 1, Position.Column + 1);
                if (Board.validPosition(pos) && hasEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                //#Special Move - En Passant
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    Position right = new Position(Position.Line, Position.Column + 1);

                    if (Board.validPosition(left) && hasEnemy(left) && Board.Piece(left) == Game.PossibleEnPassant)
                    {
                        mat[left.Line - 1, left.Column] = true; 
                    }
                    if (Board.validPosition(right) && hasEnemy(right) && Board.Piece(right) == Game.PossibleEnPassant)
                    {
                        mat[right.Line - 1, right.Column] = true;
                    }
                }
            }
            else
            {
                pos.defineValues(Position.Line + 1, Position.Column);
                if (Board.validPosition(pos) && free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.defineValues(Position.Line + 2, Position.Column);
                if (Board.validPosition(pos) && free(pos) && MoveCount == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.defineValues(Position.Line + 1, Position.Column - 1);
                if (Board.validPosition(pos) && hasEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.defineValues(Position.Line + 1, Position.Column + 1);
                if (Board.validPosition(pos) && hasEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                //#Special Move - En Passant
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    Position right = new Position(Position.Line, Position.Column + 1);

                    if (Board.validPosition(left) && hasEnemy(left) && Board.Piece(left) == Game.PossibleEnPassant)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    if (Board.validPosition(right) && hasEnemy(right) && Board.Piece(right) == Game.PossibleEnPassant)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}