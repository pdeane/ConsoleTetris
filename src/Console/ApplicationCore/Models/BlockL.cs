using DxM.ConsoleTetris.ApplicationCore.Interfaces;
using System;

namespace DxM.ConsoleTetris.ApplicationCore.Models
{
    public class BlockL : IPiece
    {
        public ConsoleColor ConsoleColor => ConsoleColor.Magenta;
        public bool[,] Matrix { get; private set; }

        private bool IsShape1 => Matrix.GetLength(0) == 3 && !Matrix[0, 1];

        private bool IsShape2 => Matrix.GetLength(0) == 2 && Matrix[0, 0];

        private bool IsShape3 => Matrix.GetLength(0) == 3 && Matrix[0, 1];

        private bool IsShape4 => Matrix.GetLength(0) == 2 && !Matrix[0, 0];

        private static bool[,] Shape1 => new[,]
        {
            // ██
            // ██
            // ████
            { true,  false },
            { true,  false },
            { true,  true  },
        };

        private static bool[,] Shape2 => new[,]
        {
            // ██████
            // ██
            { true,  true,  true  },
            { true,  false, false },
        };

        private static bool[,] Shape3 => new[,]
        {
            // ████
            //   ██
            //   ██
            { true,  true },
            { false, true },
            { false, true },
        };

        private static bool[,] Shape4 => new[,]
        {
            //     ██
            // ██████
            { false, false, true  },
            { true,  true,  true  },
        };

        public BlockL()
        {
            Matrix = Shape1;
        }

        public void RotateCCW()
        {
            Matrix = IsShape1 ? Shape4 : IsShape4 ? Shape3 : IsShape3 ? Shape2 : Shape1;
        }

        public void RotateCW()
        {
            Matrix = IsShape1 ? Shape2 : IsShape2 ? Shape3 : IsShape3 ? Shape4 : Shape1;
        }
    }
}
