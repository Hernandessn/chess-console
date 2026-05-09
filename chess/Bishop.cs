using board;

namespace chess
{
    class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color) { }
        public override string ToString()
        {
            return "B";
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

            // northeast
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            while (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) break;
                pos.Line--;
                pos.Column++;
            }

            // southeast
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            while (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) break;
                pos.Line++;
                pos.Column++;
            }

            // southwest
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            while (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) break;
                pos.Line++;
                pos.Column--;
            }

            // northwest
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            while (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) break;
                pos.Line--;
                pos.Column--;
            }

            return mat;
        }
    }
}