using DxM.ConsoleTetris.ApplicationCore.Interfaces;
using System;

namespace DxM.ConsoleTetris.ApplicationCore.Models
{
    public class BlockS : IPiece
    {
        public ConsoleColor ConsoleColor => ConsoleColor.Green;
        public bool[,] Matrix { get; private set; }

        private bool IsShape1 => Matrix.GetLength(0) == 2;

        private static bool[,] Shape1 => new[,]
        {
            //   ████
            // ████
            { false, true,  true  },
            { true,  true,  false },
        };

        private static bool[,] Shape2 => new[,]
        {
            // ██
            // ████
            //   ██
            { true,  false },
            { true,  true  },
            { false, true  },
        };

        public BlockS()
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
