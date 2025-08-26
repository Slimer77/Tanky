using System;
using Tanky;

namespace Tanky
{
    class Program
    {
        static void Main(string[] args)
        {
            var gm = new GameMap();
            try
            {
                Console.SetWindowSize(Math.Min(160, gm.Width + 2), Math.Min(50, gm.Height + 6));
                Console.SetBufferSize(Math.Max(120, gm.Width + 2), Math.Max(50, gm.Height + 8));
            }
            catch { }

            var game = new Game();
            game.Run();
        }
    }
}