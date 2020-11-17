using DxM.ConsoleTetris.ApplicationCore.Interfaces;
using DxM.ConsoleTetris.ApplicationCore.Services;
using System;

namespace DxM.ConsoleTetris.ApplicationCore.Models
{
    public class Board
    {
        public BoardSquare[,] Matrix { get; }

        public Board()
        {
            Matrix = new BoardSquare[20, 10]; // linhas , colunas
            for (int line = 0; line < 10; line++)
            {
                for (int column = 0; column < 20; column++)
                {
                    Matrix[column, line] = new();
                }
            }
        }

        public void AddPiece(IPiece piece, int x, int y)
        {
            for (int line = 0; line < piece.Lines; line++)
            {
                for (int column = 0; column < piece.Columns; column++)
                {
                    if (piece.Matrix[line, column])
                    {
                        BoardSquare boardSquare = Matrix[y + line, x + column];
                        boardSquare.ConsoleColor = piece.ConsoleColor;
                        boardSquare.Used = true;
                    }
                }
            }
        }

        public bool CanPlacePiece(IPiece piece, int x, int y)
        {
            int lines = piece.Lines;
            int columns = piece.Columns;
            if (lines + y > Matrix.GetLength(0) || x < 0 || columns + x > Matrix.GetLength(1))
            {
                return false;
            }
            bool fail = false;
            for (int line = 0; line < lines; line++)
            {
                for (int column = 0; column < columns; column++)
                {
                    fail |= piece.Matrix[line, column] && Matrix[y + line, x + column].Used;
                }
            }
            return !fail;
        }

        public int RemoveFullLines()
        {
            int linesRemoved = 0;
            for (int line = 0; line < 20; line++)
            {
                bool fullLine = Matrix[line, 0].Used &&
                                Matrix[line, 1].Used &&
                                Matrix[line, 2].Used &&
                                Matrix[line, 3].Used &&
                                Matrix[line, 4].Used &&
                                Matrix[line, 5].Used &&
                                Matrix[line, 6].Used &&
                                Matrix[line, 7].Used &&
                                Matrix[line, 8].Used &&
                                Matrix[line, 9].Used;
                if (fullLine)
                {
                    BoardSquare[,] newLine = new BoardSquare[1, 10] { { new(), new(), new(), new(), new(), new(), new(), new(), new(), new() } };
                    if (line != 0)
                    {
                        Array.Copy(Matrix, 0, Matrix, 10, 10 * line);
                    }
                    Array.Copy(newLine, 0, Matrix, 0, 10);
                    linesRemoved += 1;
                }
            }
            return linesRemoved;
        }
    }
}
