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
                PositonChess pos = new PositonChess('c', 7);
                Console.WriteLine(pos);

                Console.WriteLine(pos.ToPosition());
            }
            catch (BoardException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            Console.ReadLine();
        }
    }
}