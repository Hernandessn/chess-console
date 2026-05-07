namespace board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int NumberOfMovements { get; protected set; }
        public Board Board { get; protected set; }


        public Piece(Board board, Color color)
        {
            Position = null;
            Board = board;
            Color = color;
        }

        public void IncrementNbOfMoviment()
        {
            NumberOfMovements++;
        }

        public bool ExistMovimentsPossibles()
        {
            bool[,] mat = PossibleMoviments();

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

        public bool CanMoveTo(Position pos)
        {
            return PossibleMoviments()[pos.Line, pos.Column];
        }

        public abstract bool[,] PossibleMoviments();
    }
}
