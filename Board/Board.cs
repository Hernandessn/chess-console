namespace board
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] _pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            _pieces = new Piece[lines, columns];
        }
        public Piece piece(int line, int column)
        {
            return _pieces[line, column];
        }

        public Piece piece(Position pos)
        {
            return _pieces[pos.Line, pos.Column];
        }

        public bool ExistPiece(Position pos)
        {
            ValidatePosition(pos);
            return piece(pos) != null;
        }

        public void ToPutPiece(Piece p, Position pos)
        {
            if (ExistPiece(pos))
            {
                throw new BoardException("There is already a piece in that position!");
            }
            _pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }

        public bool PositionValidation(Position pos)
        {
            if (pos.Line < 0 || pos.Line >= Lines || pos.Column < 0 || pos.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!PositionValidation(pos))
            {
                throw new BoardException("Invalid position!");
            }
        }
    }
}
