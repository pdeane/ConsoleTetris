using DxM.ConsoleTetris.ApplicationCore.Services;
using System;
using System.Threading.Tasks;

namespace DxM.ConsoleTetris
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.CursorVisible = false;
            await new ConsoleTetrisService().PlayInteractiveModeAsync();
            Console.Read();
        }
    }
}
