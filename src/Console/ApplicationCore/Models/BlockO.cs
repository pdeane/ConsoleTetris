using DxM.ConsoleTetris.ApplicationCore.Interfaces;
using System;

namespace DxM.ConsoleTetris.ApplicationCore.Models
{
    public class BlockO : IPiece
    {
        public ConsoleColor ConsoleColor => ConsoleColor.Blue;

        public bool[,] Matrix { get; }

        public BlockO()
        {
            // ████
            // ████
            Matrix = new[,]
            {
                { true,  true },
                { true,  true },
            };
        }

        public void RotateCCW()
        {
            // Box rotated = same box
            return;
        }

        public void RotateCW()
        {
            // Box rotated = same box
            return;
        }
    }
}
