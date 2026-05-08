using board;

namespace chess
{
    class ChessGame
    {
        public Board Board { get; private set; }
        public int Shift { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _captured;
        public bool Xeque { get; private set; }

        public ChessGame()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Xeque = false;
            _pieces = new HashSet<Piece>();
            _captured = new HashSet<Piece>();
            Putpieces();
        }

        public Piece PerformsMoviment(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncrementNbOfMoviment();
            Piece pieceCaptured = Board.RemovePiece(destination);
            Board.ToPutPiece(p, destination);
            if (pieceCaptured != null)
            {
                _captured.Add(pieceCaptured);
            }
            return pieceCaptured;
        }

        public void UndoMove(Position origin, Position destination, Piece pieceCaptured)
        {
            Piece p = Board.RemovePiece(destination);
            p.DecrementNbOfMoviment();
            if (pieceCaptured != null)
            {
                Board.ToPutPiece(pieceCaptured, destination);
                _captured.Remove(pieceCaptured);
            }
            Board.ToPutPiece(p, origin);

        }
        public void TheMadePlay(Position origin, Position destination)
        {
            Piece pieceCaptured = PerformsMoviment(origin, destination);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(origin, destination, pieceCaptured);
                throw new BoardException("You can't put yourself in check");
            }

            if (IsInCheck(Opponent(CurrentPlayer)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (CheckmateTest(Opponent(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Shift++;
                ChangePlayer();
            }
        }

        public void validationPositionOrigin(Position pos)
        {
            if (Board.Piece(pos) == null)
            {
                throw new BoardException("Not existing piece in position the chosen origin!");
            }
            if (CurrentPlayer != Board.Piece(pos).Color)
            {
                throw new BoardException("The item of origin you've chosen is not yours!");
            }
            if (!Board.Piece(pos).ExistMovimentsPossibles())
            {
                throw new BoardException("There are no possible moves for the chosen origin piece!");
            }
        }

        public void ValidationPositionDestination(Position origin, Position destination)
        {
            if (!Board.Piece(origin).PossibleMovement(destination))
            {
                throw new BoardException("Position destination invalid!");
            }
        }
        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        public HashSet<Piece> PiecesCaptured(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in _captured)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> PiecesInPlay(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in _pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PiecesCaptured(color));
            return aux;
        }

        private Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece King(Color color)
        {
            foreach (Piece x in PiecesInPlay(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece K = King(color);
            if (K == null)
            {
                throw new BoardException($"There is no king of that {color} on the board");
            }
            foreach (Piece x in PiecesInPlay(Opponent(color)))
            {
                bool[,] mat = x.PossibleMoviments();
                if (mat[K.Position.Line, K.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckmateTest(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach (Piece x in PiecesInPlay(color))
            {
                bool[,] mat = x.PossibleMoviments();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Lines; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
                            Position destination = new Position(i, j);
                            Piece pieceCaptured = PerformsMoviment(origin, destination);
                            bool testCheck = IsInCheck(color);
                            UndoMove(origin, destination, pieceCaptured);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PutNewPiece(char column, int line, Piece piece)
        {
            Board.ToPutPiece(piece, new PositonChess(column, line).ToPosition());
            _pieces.Add(piece);
        }
        private void Putpieces()
        {
            PutNewPiece('c', 1, new Tower(Board, Color.White));
            PutNewPiece('d', 1, new King(Board, Color.White));
            PutNewPiece('h', 7, new Tower(Board, Color.White));

            PutNewPiece('a', 8, new King(Board, Color.Black));
            PutNewPiece('b', 8, new Tower(Board, Color.Black));

        }
    }
}
