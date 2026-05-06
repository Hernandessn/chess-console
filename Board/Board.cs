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
        public void ToPutPiece(Piece p, Position pos)
        {
            _pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }
    }
}
