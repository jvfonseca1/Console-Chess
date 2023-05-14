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

        public override bool[,] possibleMoves()
        {
            throw new NotImplementedException();
        }
    }
}