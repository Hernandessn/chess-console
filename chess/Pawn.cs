using board;

namespace chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color) { }
        public override string ToString()
        {
            return "P";
        }

        private bool CanCapture(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p.Color != Color;
        }

        public override bool[,] PossibleMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);
            if (Color == Color.White)
            {
                // 1 square forward
                pos.SetValues(Position.Line - 1, Position.Column);
                if (Board.PositionValidation(pos) && Board.Piece(pos) == null)
                    mat[pos.Line, pos.Column] = true;

                // 2 squares on first move
                pos.SetValues(Position.Line - 2, Position.Column);
                if (Board.PositionValidation(pos) && Board.Piece(pos) == null && NumberOfMovements == 0)
                    mat[pos.Line, pos.Column] = true;

                // capture left diagonal
                pos.SetValues(Position.Line - 1, Position.Column - 1);
                if (Board.PositionValidation(pos) && CanCapture(pos))
                    mat[pos.Line, pos.Column] = true;

                // capture right diagonal
                pos.SetValues(Position.Line - 1, Position.Column + 1);
                if (Board.PositionValidation(pos) && CanCapture(pos))
                    mat[pos.Line, pos.Column] = true;
            }
            else // Black
            {
                // 1 square forward
                pos.SetValues(Position.Line + 1, Position.Column);
                if (Board.PositionValidation(pos) && Board.Piece(pos) == null)
                    mat[pos.Line, pos.Column] = true;

                // 2 squares on first move
                pos.SetValues(Position.Line + 2, Position.Column);
                if (Board.PositionValidation(pos) && Board.Piece(pos) == null && NumberOfMovements == 0)
                    mat[pos.Line, pos.Column] = true;

                // capture left diagonal
                pos.SetValues(Position.Line + 1, Position.Column - 1);
                if (Board.PositionValidation(pos) && CanCapture(pos))
                    mat[pos.Line, pos.Column] = true;

                // capture right diagonal
                pos.SetValues(Position.Line + 1, Position.Column + 1);
                if (Board.PositionValidation(pos) && CanCapture(pos))
                    mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }
    }
}