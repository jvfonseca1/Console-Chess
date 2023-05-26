using board;

namespace chess
{
    internal class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {
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
            }

            return mat;
        }
    }
}