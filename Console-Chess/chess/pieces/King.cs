using board;

namespace chess
{
    internal class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "\u265A";
        }

        public override bool[,] possibleMoves()
        {
            throw new NotImplementedException();
        }
    }
}
