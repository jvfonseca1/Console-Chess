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

        public override bool[,] possibleMoves()
        {
            throw new NotImplementedException();
        }
    }
}