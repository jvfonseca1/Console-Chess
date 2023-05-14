﻿namespace board
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

        public abstract bool [,] possibleMoves ();

        public void addMove ()
        {
            MoveCount++;
        }
    }
}
