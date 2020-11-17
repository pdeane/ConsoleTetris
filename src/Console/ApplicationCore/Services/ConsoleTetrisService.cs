using DxM.ConsoleTetris.ApplicationCore.Interfaces;
using DxM.ConsoleTetris.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace DxM.ConsoleTetris.ApplicationCore.Services
{
    public class ConsoleTetrisService
    {
        private const int BoardLeft = 15;
        private const int BoardTop = 6;

        private Board Board { get; }
        private Queue<IPiece> NextPieces { get; }
        private Random Random { get; }
        private object TetrisLock { get; }

        public ConsoleTetrisService()
        {
            Board = new Board();
            NextPieces = new Queue<IPiece>();
            Random = new Random(DateTime.Now.Millisecond);
            TetrisLock = new object();
            for (int it = 0; it < 3; it++)
            {
                IPiece piece = ChooseRandomPiece();
                NextPieces.Enqueue(piece);
            }
        }

        public async Task PlayInteractiveModeAsync()
        {
            Console.Clear();
            PrintService.PrintTitle();
            PrintService.PrintBoard(Board);
            PrintService.PrintBoardInfo();
            PrintService.PrintLevel(1);
            PrintService.PrintScore(0);

            IPiece piece = null;
            bool pieceStillDropping = true;
            int pieceLines = 0;
            int pieceColumns = 0;
            int linesRemoved = 0;
            int left = 0;
            int top = 0;
            int level = 1;
            int score = 0;
            Timer timer = new Timer
            {
                Interval = 700,
            };
            timer.Elapsed += (sender, e) =>
            {
                lock (TetrisLock)
                {
                    bool canMoveDown = top < 20 && Board.CanPlacePiece(piece, left, top + 1);
                    if (canMoveDown)
                    {
                        top += 1;
                        PrintService.PrintBoard(Board);
                        PrintService.PrintPiece(piece, BoardLeft + (left * 2), BoardTop + top);
                    }
                    else
                    {
                        pieceStillDropping = false;
                    }
                }
            };

            // LOOP NEW PIECE
            while (true)
            {
                IPiece newPiece = ChooseRandomPiece();
                NextPieces.Enqueue(newPiece);
                piece = NextPieces.Dequeue();
                RefreshNext3Pieces();
                bool canPlacePiece = Board.CanPlacePiece(piece, 0, 0);
                if (!canPlacePiece)
                {
                    PrintService.PrintEndMessage();
                    return;
                }
                pieceLines = piece.Lines;
                pieceColumns = piece.Columns;

                // LOOP DROP PIECE
                PrintService.PrintBoard(Board);
                PrintService.PrintPiece(piece, BoardLeft, BoardTop);
                timer.Start();
                pieceStillDropping = true;
                left = 0;
                top = 0;
                while (pieceStillDropping)
                {
                    if (Console.KeyAvailable)
                    {
                        lock (TetrisLock)
                        {
                            ConsoleKey consoleKey = Console.ReadKey().Key;
                            switch (consoleKey)
                            {
                                case ConsoleKey.LeftArrow:
                                    bool canMoveLeft = Board.CanPlacePiece(piece, left - 1, top);
                                    left -= canMoveLeft ? 1 : 0;
                                    break;
                                case ConsoleKey.RightArrow:
                                    bool canMoveRight = Board.CanPlacePiece(piece, left + 1, top);
                                    left += canMoveRight ? 1 : 0;
                                    break;
                                case ConsoleKey.Q:
                                    piece.RotateCCW();
                                    pieceLines = piece.Lines;
                                    pieceColumns = piece.Columns;
                                    bool canRotateCCW = Board.CanPlacePiece(piece, left, top);
                                    if (!canRotateCCW)
                                    {
                                        piece.RotateCW();
                                        pieceLines = piece.Lines;
                                        pieceColumns = piece.Columns;
                                    }
                                    break;
                                case ConsoleKey.W:
                                    piece.RotateCW();
                                    pieceLines = piece.Lines;
                                    pieceColumns = piece.Columns;
                                    bool canRotateCW = Board.CanPlacePiece(piece, left, top);
                                    if (!canRotateCW)
                                    {
                                        piece.RotateCCW();
                                        pieceLines = piece.Lines;
                                        pieceColumns = piece.Columns;
                                    }
                                    break;
                            }
                            PrintService.PrintBoard(Board);
                            PrintService.PrintPiece(piece, BoardLeft + (left * 2), BoardTop + top);
                        }
                    }
                    await Task.Delay(50);
                }
                timer.Stop();
                Board.AddPiece(piece, left, top);
                int lines = Board.RemoveFullLines();
                score += lines switch
                {
                    0 => 0,
                    1 => 10,
                    2 => 30,
                    3 => 50,
                    4 => 100,
                    _ => throw new ArgumentException("Something bad has occured...")
                };
                linesRemoved += lines;
                PrintService.PrintScore(score);
                if (level < 9 && linesRemoved >= 5)
                {
                    linesRemoved = 0;
                    timer.Interval -= 50;
                    level += 1;
                    PrintService.PrintLevel(level);
                }
            }
        }

        private IPiece ChooseRandomPiece()
        {
            return Random.Next(7) switch
            {
                0 => new BlockI(),
                1 => new BlockJ(),
                2 => new BlockL(),
                3 => new BlockO(),
                4 => new BlockS(),
                5 => new BlockT(),
                6 => new BlockZ(),
                _ => throw new ArgumentException("Invalid block!")
            };
        }

        private void RefreshNext3Pieces()
        {
            PrintService.ClearNext3Pieces();
            PrintService.PrintNext3Pieces(NextPieces);
        }
    }
}
