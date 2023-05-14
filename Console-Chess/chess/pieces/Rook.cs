using board;

namespace chess
{
    internal class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "\u265C";
        }

        private bool canMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != this.Color;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            //up
            pos.defineValues(pos.Line - 1, pos.Column);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Line = pos.Line - 1;
            }

            //down
            pos.defineValues(pos.Line - 1, pos.Column);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;

                if (Board.Piece(pos) != null && Board.Piece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Line += 1;
            }

            //left
            pos.defineValues(pos.Line, pos.Column - 1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;

                if (Board.Piece(pos) != null && Board.Piece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Column -= 1;
            }

            //right
            pos.defineValues(pos.Line, pos.Column + 1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;

                if (Board.Piece(pos) != null && Board.Piece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Column += 1;
            }

            return mat;
        }
    }
}