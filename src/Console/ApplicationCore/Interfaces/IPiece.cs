using System;

namespace DxM.ConsoleTetris.ApplicationCore.Interfaces
{
    public interface IPiece
    {
        int Columns => Matrix.GetLength(1);
        ConsoleColor ConsoleColor { get; }
        int Lines => Matrix.GetLength(0);
        bool[,] Matrix { get; }
        void RotateCW();
        void RotateCCW();
    }
}
