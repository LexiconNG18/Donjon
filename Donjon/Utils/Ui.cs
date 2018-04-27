using System;
using System.Collections.Generic;
using System.Linq;
using Donjon.Entities;
using Donjon.Entities.Creatures;

namespace Donjon.Utils {
    class Ui {
        private readonly Log log;
        private readonly Map map;

        public Ui(Log log, Map map) {
            this.log = log;
            this.map = map;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
        }

        public T MenuSelect<T>(string heading, IList<T> items) where T : class, IDrawable {
            // Todo: handle count > 9, e.g. with A, B, C, etc

            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorTop       = 0;

            var left = map.Width * 2 + 1;
            WriteLineAt(left, heading);
            WriteItemsAt(left, items);
            WriteLineAt(left, " 0) Never mind");
            var item = PickFromList(items);

            MenuClear();
            return item;
        }


        public void MenuWrite(IEnumerable<string> strings) {
            Console.CursorTop       = 0;
            Console.ForegroundColor = ConsoleColor.White;
            var left = map.Width * 2 + 1;
            foreach (var s in strings) WriteLineAt(left, s);
        }

        public void MenuList<T>(string heading, IEnumerable<T> items) where T : class, IDrawable {
            var itemList = items.ToList();
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorTop       = 0;

            var left = map.Width * 2 + 1;
            WriteLineAt(left, heading);
            WriteItemsAt(left, itemList, unnumbered: true);
        }

        public void MenuClear() { ClearAt(map.Width * 2 + 1, map.Height); }

        private void ClearAt(int left, int rows) {
            Console.CursorTop = 0;
            for (var i = 0; i < rows; i++) WriteLineAt(left);
        }

        private void WriteItemsAt<T>(int left, IEnumerable<T> items, bool unnumbered = false)
            where T : class, IDrawable {
            var resetColor = Console.ForegroundColor;
            Console.CursorLeft = left;
            var i = 0;
            foreach (var item in items) {
                i++;
                var bullet = unnumbered ? "  - " : $"{i,2}) ";

                Console.ForegroundColor = ConsoleColor.White;
                WriteAt(left, bullet);

                Console.ForegroundColor = item.Color;
                WriteLine(item.Name);
            }

            if (i == 0) WriteLineAt(left, " ...nothing");
            Console.ForegroundColor = resetColor;
        }

        private static T PickFromList<T>(IList<T> items) where T : class, IDrawable {
            T    item = null;
            bool parsed;
            do {
                var answer = Console.ReadKey(intercept: true).KeyChar.ToString();
                parsed = int.TryParse(answer, out var pick) && pick <= items.Count;
                if (!parsed) continue;

                item = pick > 0 ? items[pick - 1] : null;
            } while (!parsed);

            return item;
        }


        private void WriteLine(string message = "", bool fill = true) {
            WriteLineAt(Console.CursorLeft, message, fill);
        }

        private void Write(string message = "", bool fill = false) { WriteAt(Console.CursorLeft, message, fill); }

        private void WriteLineAt(int left, string message = "", bool fill = true) {
            WriteAt(left, message, fill);
            Console.WriteLine();
        }

        private void WriteAt(int left, string message = "", bool fill = false) {
            Console.CursorLeft = left;
            Console.Write(fill ? message.Fill(left) : message);
        }


        public void DrawMap() {
            Console.SetCursorPosition(left: 0, top: 0);

            Console.BackgroundColor = ConsoleColor.Black;
            for (var y = 0; y < map.Height; y++) {
                for (var x = 0; x < map.Width; x++) {
                    var cell       = map.Cell(x, y);
                    var appearance = cell.Appearance;
                    Console.ForegroundColor = appearance.Color;
                    Console.BackgroundColor = cell.Environment.Background;
                    Console.Write(" " + appearance.Symbol);
                }

                Console.WriteLine();
            }
        }

        public void DrawStatus(Hero hero) {
            ConsoleColor fg;
            switch (2 * hero.Health / hero.MaxHealth) {
            case 0:
                fg = ConsoleColor.Red;
                break;
            case 1:
                fg = ConsoleColor.Yellow;
                break;
            case 2:
                fg = ConsoleColor.Green;
                break;
            default:
                fg = ConsoleColor.White;
                break;
            }


            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Write("Health:");


            Console.ForegroundColor = fg;
            Write($" {hero.Health,-2}");

            Console.ForegroundColor = ConsoleColor.White;
            Write($"/{hero.MaxHealth,-2}");
            Write($"  {hero.Wielded?.Name ?? "Fists"}: {hero.Damage} ");
            WriteLine();

            Console.BackgroundColor = ConsoleColor.Black;
            WriteLine();
        }

        public void ReStart() {
            Console.CursorVisible   = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(left: 0, top: 0);
        }


        public void WriteLog() {
            var maxLength = Console.WindowWidth;
            var indent    = Math.Max(val1: 2, val2: log.Count.ToString().Length) + 2;

            var oldIndex = 0;
            var newIndex = log.Old.Count();

            var logOld = log.Old.Select(s => $"{oldIndex++,2}: {s}").Reflow(maxLength, indent);
            var logNew = log.New.Select(s => $"{newIndex++,2}: {s}").Reflow(maxLength, indent);

            var height   = Console.WindowHeight - Console.CursorTop - 1;
            var oldLines = Math.Max(val1: 0, val2: height - logNew.Count);
            var oldSkip  = Math.Max(val1: 0, val2: logOld.Count - oldLines);

            var newLines = Math.Min(height, logNew.Count);
            var newSkip  = Math.Max(val1: 0, val2: logNew.Count - newLines);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (var line in logOld.Skip(oldSkip)) WriteLine(line);

            Console.ForegroundColor = ConsoleColor.White;
            foreach (var line in logNew.Skip(newSkip)) WriteLine(line);
        }
    }
}