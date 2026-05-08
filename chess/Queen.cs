using board;

namespace chess
{
    class Queen : Piece
    {
        public Queen(Board board, Color color) : base(board, color) { }
        public override string ToString()
        {
            return "Q";
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
            pos.SetValues(Position.Line - 1, Position.Column);
            while (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) break;
                pos.Line--;
            }

            // northeast
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            while (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) break;
                pos.Line--;
                pos.Column++;
            }

            // right
            pos.SetValues(Position.Line, Position.Column + 1);
            while (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) break;
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

            // below
            pos.SetValues(Position.Line + 1, Position.Column);
            while (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) break;
                pos.Line++;
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

            // left
            pos.SetValues(Position.Line, Position.Column - 1);
            while (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) break;
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