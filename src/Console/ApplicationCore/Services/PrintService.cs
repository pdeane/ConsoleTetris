using DxM.ConsoleTetris.ApplicationCore.Interfaces;
using DxM.ConsoleTetris.ApplicationCore.Models;
using System;
using System.Collections.Generic;

namespace DxM.ConsoleTetris.ApplicationCore.Services
{
    public class PrintService
    {
        private const int BoardRight = 34;

        private static object PrinterLock { get; }

        static PrintService()
        {
            PrinterLock = new object();
        }

        public static void ClearNext3Pieces()
        {
            lock (PrinterLock)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(1, 10);
                Console.Write("           ");
                Console.SetCursorPosition(1, 11);
                Console.Write("           ");
                Console.SetCursorPosition(1, 12);
                Console.Write("           ");
                Console.SetCursorPosition(1, 16);
                Console.Write("           ");
                Console.SetCursorPosition(1, 17);
                Console.Write("           ");
                Console.SetCursorPosition(1, 18);
                Console.Write("           ");
                Console.SetCursorPosition(1, 22);
                Console.Write("           ");
                Console.SetCursorPosition(1, 23);
                Console.Write("           ");
                Console.SetCursorPosition(1, 24);
                Console.Write("           ");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public static void PrintBoard(Board board)
        {
            lock (PrinterLock)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.SetCursorPosition(14, 5);
                Console.Write("┌────────────────────┐");
                Console.SetCursorPosition(14, 6);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 7);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 8);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 9);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 10);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 11);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 12);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 13);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 14);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 15);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 16);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 17);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 18);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 19);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 20);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 21);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 22);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 23);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 24);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 25);
                Console.Write("│                    │");
                Console.SetCursorPosition(14, 26);
                Console.Write("└────────────────────┘");
                for (int line = 0; line < 20; line++)
                {
                    for (int column = 0; column < 10; column++)
                    {
                        if (board.Matrix[line, column].Used)
                        {
                            Console.ForegroundColor = board.Matrix[line, column].ConsoleColor;
                            Console.SetCursorPosition(15 + (column * 2), 6 + line);
                            Console.Write("██");
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public static void PrintBoardInfo()
        {
            lock (PrinterLock)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(1, 6);
                Console.Write("Next Pieces");
                Console.SetCursorPosition(1, 7);
                Console.Write("═══════════");
                Console.SetCursorPosition(41, 6);
                Console.Write("Score");
                Console.SetCursorPosition(38, 7);
                Console.Write("═══════════");
                Console.SetCursorPosition(41, 16);
                Console.Write("Level");
                Console.SetCursorPosition(38, 17);
                Console.Write("═══════════");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public static void PrintEndMessage()
        {
            lock (PrinterLock)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(13, 13);
                Console.Write("╔══════════════════════╗");
                Console.SetCursorPosition(13, 14);
                Console.Write("║                      ║");
                Console.SetCursorPosition(13, 15);
                Console.Write("║ Press enter to exit. ║");
                Console.SetCursorPosition(13, 16);
                Console.Write("║                      ║");
                Console.SetCursorPosition(13, 17);
                Console.Write("╚══════════════════════╝");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public static void PrintLevel(int level)
        {
            lock (PrinterLock)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(43, 19);
                Console.Write(level);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public static void PrintNext3Pieces(Queue<IPiece> pieces)
        {
            IPiece[] piecesArray = pieces.ToArray();
            // SLOT 1 => top 10
            int leftSlot1 = 3 + PrintNextPieceOffset(piecesArray[0]);
            PrintPiece(piecesArray[0], leftSlot1, 10);
            // SLOT 2 => top 16
            int leftSlot2 = 3 + PrintNextPieceOffset(piecesArray[1]);
            PrintPiece(piecesArray[1], leftSlot2, 16);
            // SLOT 3 => top 22
            int leftSlot3 = 3 + PrintNextPieceOffset(piecesArray[2]);
            PrintPiece(piecesArray[2], leftSlot3, 22);
        }

        public static int PrintPiece(IPiece piece, int left, int top)
        {
            lock (PrinterLock)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                int lines = piece.Lines;
                int columns = piece.Columns;
                if ((left + (2 * columns)) > BoardRight)
                {
                    left = BoardRight - ((columns - 1) * 2) - 1;
                }
                Console.ForegroundColor = piece.ConsoleColor;
                for (int line = 0; line < lines; line++)
                {
                    for (int column = 0; column < columns; column++)
                    {
                        if (piece.Matrix[line, column])
                        {
                            Console.SetCursorPosition(left + (column * 2), top + line);
                            Console.Write("██");
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            return left;
        }

        public static void PrintScore(int score)
        {
            lock (PrinterLock)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                string scoreText = $"{score:n0}";
                Console.SetCursorPosition(43 - (scoreText.Length / 2), 9);
                Console.Write(scoreText);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public static void PrintTitle()
        {
            lock (PrinterLock)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("               == CONSOLE TETRIS ==               ");
                Console.WriteLine("         Minimun size needed 50x30 console        ");
                Console.WriteLine("             Move with <- and -> keys             ");
                Console.WriteLine("           Q rotate CCW and W rotate CW           ");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(23, 2);
                Console.Write("<-");
                Console.SetCursorPosition(30, 2);
                Console.Write("->");
                Console.SetCursorPosition(11, 3);
                Console.Write("Q");
                Console.SetCursorPosition(28, 3);
                Console.Write("W");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        private static int PrintNextPieceOffset(IPiece piece)
        {
            int offset = piece switch
            {
                BlockI => 0,
                BlockS or BlockT or BlockZ => 1,
                BlockJ or BlockL or BlockO => 2,
                _ => throw new ArgumentException("Invalid block!")
            };
            return offset;
        }
    }
}
