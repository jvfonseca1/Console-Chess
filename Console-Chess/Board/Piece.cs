namespace board
{
    internal abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MoveCount { get; protected set; }
        public Board Board { get; protected set; }

        public Piece (Board board, Color color)
        {
            Position = null;
            Board = board;
            Color = color;
            MoveCount = 0;
        }

        public bool hasMoves ()
        {
            bool [,] mat = possibleMoves();
            for (int i = 0; i < Board.Lines; i++) 
            {
                for (int j = 0; j < Board.Columns; j++) 
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public abstract bool [,] possibleMoves ();

        public bool canMoveTo (Position pos)
        {
            return possibleMoves()[pos.Line, pos.Column];
        }

        public void addMove ()
        {
            MoveCount++;
        }

        public void removeMove ()
        {
            MoveCount--;
        }
    }
}
