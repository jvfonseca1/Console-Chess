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
            
            return mat;
        }
    }
}