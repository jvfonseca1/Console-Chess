using board;

namespace chess
{
    internal class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "\u265D";
        }

        public override bool[,] possibleMoves()
        {
            throw new NotImplementedException();
        }
    }
}