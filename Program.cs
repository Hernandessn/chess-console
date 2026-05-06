using board;
using chess;
using chess_console;

namespace chess_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.ToPutPiece(new Tower(board, Color.Black), new Position(0,0 ));
            board.ToPutPiece(new Tower(board, Color.Black), new Position(1, 3));
            board.ToPutPiece(new King(board, Color.Black), new Position(2, 4));

            Screen.PrintBoard(board);

            Console.ReadLine();
        }
    }
}