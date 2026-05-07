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

        public ChessGame()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            _pieces = new HashSet<Piece>();
            _captured = new HashSet<Piece>();
            Putpieces();
        }

        public void PerformsMoviment(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncrementNbOfMoviment();
            Piece pieceCaptured = Board.RemovePiece(destination);
            Board.ToPutPiece(p, destination);
            if (pieceCaptured != null)
            {
                _captured.Add(pieceCaptured);
            }
        }

        public void TheMadePlay(Position origin, Position destination)
        {
            PerformsMoviment(origin, destination);
            Shift++;
            ChangePlayer();
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
            if (!Board.Piece(origin).CanMoveTo(destination))
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

        public void PutNewPiece(char column, int line, Piece piece)
        {
            Board.ToPutPiece(piece, new PositonChess(column, line).ToPosition());
            _pieces.Add(piece);
        }
        private void Putpieces()
        {
            PutNewPiece('c', 1, new Tower(Board, Color.White));
            PutNewPiece('c', 2, new Tower(Board, Color.White));
            PutNewPiece('d', 2, new Tower(Board, Color.White));
            PutNewPiece('d', 1, new King(Board, Color.White));
            PutNewPiece('e', 1, new Tower(Board, Color.White));
            PutNewPiece('e', 2, new Tower(Board, Color.White));

            PutNewPiece('c', 7, new Tower(Board, Color.Black));
            PutNewPiece('c', 8, new Tower(Board, Color.Black));
            PutNewPiece('d', 7, new Tower(Board, Color.Black));
            PutNewPiece('d', 8, new King(Board, Color.Black));
            PutNewPiece('e', 7, new Tower(Board, Color.Black));
            PutNewPiece('e', 8, new Tower(Board, Color.Black));

        }
    }
}
