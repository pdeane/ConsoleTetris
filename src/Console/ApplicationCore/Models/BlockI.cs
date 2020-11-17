using DxM.ConsoleTetris.ApplicationCore.Interfaces;
using System;

namespace DxM.ConsoleTetris.ApplicationCore.Models
{
    public class BlockI : IPiece
    {
        public ConsoleColor ConsoleColor => ConsoleColor.Red;
        public bool[,] Matrix { get; private set; }

        private bool IsShape1 => Matrix.GetLength(0) == 1;

        private static bool[,] Shape1 => new[,]
        {
            // ████████
            { true,  true,  true,  true  },
        };

        private static bool[,] Shape2 => new[,]
        {
            // ██
            // ██
            // ██
            // ██
            { true },
            { true },
            { true },
            { true },
        };

        public BlockI()
        {
            Matrix = Shape1;
        }

        public void RotateCCW()
        {
            Rotate();
        }

        public void RotateCW()
        {
            Rotate();
        }

        private void Rotate()
        {
            Matrix = IsShape1 ? Shape2 : Shape1;
        }
    }
}
