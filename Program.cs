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
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(game);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadPositionChess().ToPosition();
                        game.validationPositionOrigin(origin);

                        bool[,] possiblePositions = game.Board.Piece(origin).PossibleMoviments();

                        Console.Clear();
                        Screen.PrintBoard(game.Board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destination: ");
                        Position destination = Screen.ReadPositionChess().ToPosition();
                        game.ValidationPositionDestination(origin, destination);

                        game.TheMadePlay(origin, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                        Console.ReadLine();
                    }
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