using System;

namespace Donjon {
    class Program {
        private static void Main() {
            bool playAgain;
            do {
                var game = new Game();
                game.Run();
                Console.WriteLine("Another game?");
                var key = Console.ReadKey(intercept: true).Key;
                playAgain = key == ConsoleKey.Y;
            } while (playAgain);

            Console.Clear();
        }
    }
}