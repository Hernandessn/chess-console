using board;

namespace chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(board, color) { }
        public override string ToString()
        {
            return "R";
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

            // above
            pos.SetValues(pos.Line - 1, pos.Column);
            if (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // northeast
            pos.SetValues(pos.Line - 1, pos.Column + 1);
            if (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // right
            pos.SetValues(pos.Line , pos.Column + 1);
            if (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }


            // southeast
            pos.SetValues(pos.Line + 1, pos.Column + 1);
            if (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // below
            pos.SetValues(pos.Line + 1, pos.Column);
            if (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // southwest
            pos.SetValues(pos.Line + 1, pos.Column - 1);
            if (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // left
            pos.SetValues(pos.Line, pos.Column - 1);
            if (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // northwest
            pos.SetValues(pos.Line - 1, pos.Column - 1);
            if (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }

    }
}
