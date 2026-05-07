using board;
using chess;

namespace chess_console
{
    class Screen
    {
        public static void PrintMatch(ChessGame game)
        {
            PrintBoard(game.Board);
            Console.WriteLine();
            PrintPiecesCaptured(game);
            Console.WriteLine($"Shift: {game.Shift}");
            Console.WriteLine($"Aguardando jogada: {game.CurrentPlayer}");
        }

        public static void PrintPiecesCaptured(ChessGame game)
        {
            Console.WriteLine("Pieces captured: ");
            Console.Write("Whites: ");
            PrintGroup(game.PiecesCaptured(Color.White));
            Console.WriteLine();
            ConsoleColor aux = Console.ForegroundColor;
            Console.Write("Blacks: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintGroup(game.PiecesCaptured(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void PrintGroup(HashSet<Piece> group)
        {
            Console.Write("[");
            foreach (Piece x in group)
            {
                Console.Write(x +  " ");
            }
            Console.Write("]");
        }
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void PrintBoard(Board board, bool[,] possiblePositions)
        {
            ConsoleColor OriginalBackground = Console.BackgroundColor;
            ConsoleColor NewBackground = ConsoleColor.DarkGray;
            for (int i = 0; i < board.Lines; i++)
            {
                Console.BackgroundColor = OriginalBackground;
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possiblePositions[i, j])
                        Console.BackgroundColor = NewBackground;
                    else
                        Console.BackgroundColor = OriginalBackground;

                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = OriginalBackground;
                }
                Console.BackgroundColor = OriginalBackground;
                Console.WriteLine();
            } 
            Console.BackgroundColor = OriginalBackground;
            Console.WriteLine("  a b c d e f g h");
        }

        public static PositonChess ReadPositionChess()
        {
            string s = Console.ReadLine();

            if (s.Length != 2 || !char.IsLetter(s[0]) || !char.IsDigit(s[1]))
            {
                throw new BoardException("Invalid position! Use format: a1 to h8");
            }

            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new PositonChess(column, line);
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");

            }
        }
    }
}
