using board;

namespace chess
{
    class Tower : Piece
    {
        public Tower(Board board, Color color) : base(board, color) { }
        public override string ToString()
        {
            return "T";
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
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                    break;
                pos.Line--;
            }

            // below
            pos.SetValues(Position.Line + 1, Position.Column);
            while (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                    break;
                pos.Line++;
            }

            // right
            pos.SetValues(Position.Line, Position.Column + 1);
            while (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                    break;
                pos.Column++;
            }

            // left
            pos.SetValues(Position.Line, Position.Column - 1);
            while (Board.PositionValidation(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                    break;
                pos.Column--;
            }
            return mat;
        }
    }
}
