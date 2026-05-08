using board;

namespace chess
{
    class Horse : Piece
    {
        public Horse(Board board, Color color) : base(board, color) { }
        public override string ToString()
        {
            return "H";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            // 8 L-shaped moves of the knight
            pos.SetValues(Position.Line - 2, Position.Column - 1);
            if (Board.PositionValidation(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            pos.SetValues(Position.Line - 2, Position.Column + 1);
            if (Board.PositionValidation(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            pos.SetValues(Position.Line - 1, Position.Column + 2);
            if (Board.PositionValidation(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            pos.SetValues(Position.Line + 1, Position.Column + 2);
            if (Board.PositionValidation(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            pos.SetValues(Position.Line + 2, Position.Column + 1);
            if (Board.PositionValidation(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            pos.SetValues(Position.Line + 2, Position.Column - 1);
            if (Board.PositionValidation(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            pos.SetValues(Position.Line + 1, Position.Column - 2);
            if (Board.PositionValidation(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            pos.SetValues(Position.Line - 1, Position.Column - 2);
            if (Board.PositionValidation(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            return mat;
        }
    }
}