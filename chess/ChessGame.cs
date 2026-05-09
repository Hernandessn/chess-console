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
        public Piece VulnerableEnPassant { get; private set; }

        public ChessGame()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Xeque = false;
            VulnerableEnPassant = null;
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

            // #special move small castling
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Line, origin.Column + 3);
                Position destinationT = new Position(origin.Line, origin.Column + 1);
                Piece T = Board.RemovePiece(originT);
                T.IncrementNbOfMoviment();
                Board.ToPutPiece(T, destinationT);
            }

            // #special move big castling
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Line, origin.Column - 4);
                Position destinationT = new Position(origin.Line, origin.Column - 1);
                Piece T = Board.RemovePiece(originT);
                T.IncrementNbOfMoviment();
                Board.ToPutPiece(T, destinationT);
            }

            // #special move en passant
            if (p is Pawn)
            {
                if (origin.Column != destination.Column && pieceCaptured == null)
                {
                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(destination.Line + 1, destination.Column);
                    }
                    else
                    {
                        posP = new Position(destination.Line - 1, destination.Column);
                    }
                    pieceCaptured = Board.RemovePiece(posP);
                    _captured.Add(pieceCaptured);
                }
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

            // #special move small castling
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Line, origin.Column + 3);
                Position destinationT = new Position(origin.Line, origin.Column + 1);
                Piece T = Board.RemovePiece(destinationT);
                T.DecrementNbOfMoviment();
                Board.ToPutPiece(T, originT);
            }

            // #special move big castling
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Line, origin.Column - 4);
                Position destinationT = new Position(origin.Line, origin.Column - 1);
                Piece T = Board.RemovePiece(destination);
                T.DecrementNbOfMoviment();
                Board.ToPutPiece(T, originT);
            }

            // #special move en passant
            if (p is Pawn)
            {
                if (origin.Column != destination.Column && pieceCaptured == VulnerableEnPassant)
                {
                    Piece pawn = Board.RemovePiece(destination);
                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(3, destination.Column);
                    }
                    else
                    {
                        posP = new Position(4, destination.Column);
                    }
                    Board.ToPutPiece(pawn, posP);
                }
            }

        }
        public void TheMadePlay(Position origin, Position destination)
        {
            Piece pieceCaptured = PerformsMoviment(origin, destination);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(origin, destination, pieceCaptured);
                throw new BoardException("You can't put yourself in check");
            }

            Piece p = Board.Piece(destination);

            // #special move promotion
            if (p is Pawn)
            {
                if ((p.Color == Color.White && destination.Line == 0) || (p.Color == Color.Black && destination.Line == 7))
                {
                    p = Board.RemovePiece(destination);
                    _pieces.Remove(p);
                    Piece queen = new Queen(Board, p.Color);
                    Board.ToPutPiece(queen, destination);
                    _pieces.Add(queen);
                }
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


            // #special move en passant
            if (p is Pawn && (destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2))
            {
                VulnerableEnPassant = p;
            }
            else
            {
                VulnerableEnPassant = null;
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
                    for (int j = 0; j < Board.Columns; j++)
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
            // White pieces
            PutNewPiece('a', 1, new Tower(Board, Color.White));
            PutNewPiece('b', 1, new Horse(Board, Color.White));
            PutNewPiece('c', 1, new Bishop(Board, Color.White));
            PutNewPiece('d', 1, new Queen(Board, Color.White));
            PutNewPiece('e', 1, new King(Board, Color.White, this));
            PutNewPiece('f', 1, new Bishop(Board, Color.White));
            PutNewPiece('g', 1, new Horse(Board, Color.White));
            PutNewPiece('h', 1, new Tower(Board, Color.White));
            PutNewPiece('a', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('b', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('c', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('d', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('e', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('f', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('g', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('h', 2, new Pawn(Board, Color.White, this));

            // Black pieces
            PutNewPiece('a', 8, new Tower(Board, Color.Black));
            PutNewPiece('b', 8, new Horse(Board, Color.Black));
            PutNewPiece('c', 8, new Bishop(Board, Color.Black));
            PutNewPiece('d', 8, new Queen(Board, Color.Black));
            PutNewPiece('e', 8, new King(Board, Color.Black, this));
            PutNewPiece('f', 8, new Bishop(Board, Color.Black));
            PutNewPiece('g', 8, new Horse(Board, Color.Black));
            PutNewPiece('h', 8, new Tower(Board, Color.Black));
            PutNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }
    }
}

