using board;

namespace chess
{
    internal class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "\u265E";
        }

        private bool canMove (Position pos)
        {
            Piece p = Board.Piece (pos);
            return p == null || p.Color != this.Color;
        }

        public override bool[,] possibleMoves()
        {
            bool [,] mat = new bool [Board.Lines, Board.Columns];

            Position pos = new Position (0, 0);

            //up - left
            pos.defineValues(Position.Line - 2, Position.Column - 1 );
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //up - right
            pos.defineValues(Position.Line - 2, Position.Column + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //left - up
            pos.defineValues(Position.Line - 1, Position.Column - 2);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //left - down
            pos.defineValues(Position.Line + 1, Position.Column - 2);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //down - left
            pos.defineValues(Position.Line + 2, Position.Column - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //down - right
            pos.defineValues(Position.Line + 2, Position.Column + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //right - down
            pos.defineValues(Position.Line - 1, Position.Column + 2);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //right - up
            pos.defineValues(Position.Line + 1, Position.Column + 2);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }
    }
}