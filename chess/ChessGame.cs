using board;

namespace chess
{
    class ChessGame
    {
        public Board Board { get; private set; }
        private int _shift;
        private Color _currentPlayer;
        public bool Finished { get; private set; }


        public ChessGame()
        {
            Board = new Board(8, 8);
            _shift = 1;
            _currentPlayer = Color.White;
            Finished = false;
            Putpieces();
        }

        public void PerformsMoviment(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncrementNbOfMoviment();
            Piece pieceCaptured = Board.RemovePiece(destination);
            Board.ToPutPiece(p, destination);

        }
        private void Putpieces()
        {
            Board.ToPutPiece(new Tower(Board, Color.White), new PositonChess('c', 1).ToPosition());
            Board.ToPutPiece(new Tower(Board, Color.White), new PositonChess('c', 2).ToPosition());
            Board.ToPutPiece(new Tower(Board, Color.White), new PositonChess('d', 2).ToPosition());
            Board.ToPutPiece(new King(Board, Color.White), new PositonChess('d', 1).ToPosition());
            Board.ToPutPiece(new Tower(Board, Color.White), new PositonChess('e', 1).ToPosition());
            Board.ToPutPiece(new Tower(Board, Color.White), new PositonChess('e', 2).ToPosition());

            Board.ToPutPiece(new Tower(Board, Color.Black), new PositonChess('c', 7).ToPosition());
            Board.ToPutPiece(new Tower(Board, Color.Black), new PositonChess('c', 8).ToPosition());
            Board.ToPutPiece(new Tower(Board, Color.Black), new PositonChess('d', 7).ToPosition());
            Board.ToPutPiece(new Tower(Board, Color.Black), new PositonChess('d', 8).ToPosition());
            Board.ToPutPiece(new Tower(Board, Color.Black), new PositonChess('e', 7).ToPosition());
            Board.ToPutPiece(new King(Board, Color.Black), new PositonChess('e', 8).ToPosition());

        }
    }
}
