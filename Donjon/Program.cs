using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donjon
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playAgain;
            do
            {
                var game = new Game();
                game.Run();
                Console.WriteLine("Another game?");
                var key = Console.ReadKey(intercept: true).Key;
                playAgain = key == ConsoleKey.Y;
            } while (playAgain);
        }
    }
}
