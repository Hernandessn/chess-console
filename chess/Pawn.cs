using board;

namespace chess
{
    class Pawn : Piece
    {
        private ChessGame _game;
        public Pawn(Board board, Color color, ChessGame game) : base(board, color) { _game = game; }

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
                Position middleW = new Position(Position.Line - 1, Position.Column);
                if (Board.PositionValidation(pos) && Board.Piece(pos) == null && Board.Piece(middleW) == null && NumberOfMovements == 0)
                    mat[pos.Line, pos.Column] = true;

                // capture left diagonal
                pos.SetValues(Position.Line - 1, Position.Column - 1);
                if (Board.PositionValidation(pos) && CanCapture(pos))
                    mat[pos.Line, pos.Column] = true;

                // capture right diagonal
                pos.SetValues(Position.Line - 1, Position.Column + 1);
                if (Board.PositionValidation(pos) && CanCapture(pos))
                    mat[pos.Line, pos.Column] = true;

                // #special move en passant white
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.PositionValidation(left) && CanCapture(left) && Board.Piece(left) == _game.VulnerableEnPassant)
                        mat[left.Line - 1, left.Column] = true;

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.PositionValidation(right) && CanCapture(right) && Board.Piece(right) == _game.VulnerableEnPassant)
                        mat[right.Line - 1, right.Column] = true;
                }
            }
            else 
            {
                // 1 square forward
                pos.SetValues(Position.Line + 1, Position.Column);
                if (Board.PositionValidation(pos) && Board.Piece(pos) == null)
                    mat[pos.Line, pos.Column] = true;

                // 2 squares on first move
                pos.SetValues(Position.Line + 2, Position.Column);
                Position middleB = new Position(Position.Line + 1, Position.Column);
                if (Board.PositionValidation(pos) && Board.Piece(pos) == null && Board.Piece(middleB) == null && NumberOfMovements == 0)
                    mat[pos.Line, pos.Column] = true;

                // capture left diagonal
                pos.SetValues(Position.Line + 1, Position.Column - 1);
                if (Board.PositionValidation(pos) && CanCapture(pos))
                    mat[pos.Line, pos.Column] = true;

                // capture right diagonal
                pos.SetValues(Position.Line + 1, Position.Column + 1);
                if (Board.PositionValidation(pos) && CanCapture(pos))
                    mat[pos.Line, pos.Column] = true;

                // #special move en passant black
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.PositionValidation(left) && CanCapture(left) && Board.Piece(left) == _game.VulnerableEnPassant)
                        mat[left.Line + 1, left.Column] = true;

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.PositionValidation(right) && CanCapture(right) && Board.Piece(right) == _game.VulnerableEnPassant)
                        mat[right.Line + 1, right.Column] = true;
                }
            }

            return mat;
        }
    }
}