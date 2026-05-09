using board;

namespace chess
{
    class King : Piece
    {
        private ChessGame _game;

        public King(Board board, Color color, ChessGame game) : base(board, color)
        {
            _game = game;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != Color;
        }

        private bool TestTowerForCastling(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p is Tower && p.Color == Color && p.NumberOfMovements == 0;
        }

        public override bool[,] PossibleMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            // above
            pos.SetValues(Position.Line - 1, Position.Column);
            if (Board.PositionValidation(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            // northeast
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            if (Board.PositionValidation(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            // right
            pos.SetValues(Position.Line, Position.Column + 1);
            if (Board.PositionValidation(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            // southeast
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            if (Board.PositionValidation(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            // below
            pos.SetValues(Position.Line + 1, Position.Column);
            if (Board.PositionValidation(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            // southwest
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            if (Board.PositionValidation(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            // left
            pos.SetValues(Position.Line, Position.Column - 1);
            if (Board.PositionValidation(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            // northwest
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            if (Board.PositionValidation(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            // roque dentro do if, usando Position (não pos)
            if (NumberOfMovements == 0 && !_game.Xeque)
            {
                // small castling
                Position posT1 = new Position(Position.Line, Position.Column + 3);
                if (TestTowerForCastling(posT1))
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null)
                        mat[Position.Line, Position.Column + 2] = true;
                }

                // big castling estava fora do if
                Position posT2 = new Position(Position.Line, Position.Column - 4);
                if (TestTowerForCastling(posT2))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null)
                        mat[Position.Line, Position.Column - 2] = true;
                }
            }

            return mat;
        }
    }
}