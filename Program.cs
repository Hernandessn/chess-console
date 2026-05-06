using board;
using chess;
using chess_console;

namespace chess_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessGame game = new ChessGame();

                while (!game.Finished)
                {
                    Console.Clear();
                    Screen.PrintBoard(game.Board);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadPositionChess().ToPosition();
                    Console.Write("Destination: ");
                    Position destination = Screen.ReadPositionChess().ToPosition();

                    game.PerformsMoviment(origin, destination);
                }
            }
            catch (BoardException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            Console.ReadLine();
        }
    }
}