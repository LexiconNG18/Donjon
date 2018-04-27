using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Donjon.Utils {
    static class StringExtensions {
        public static string Fill(this string str, int left) {
            var maxWidth = Console.WindowWidth - left - 1;
            return str.PadRight(maxWidth).Substring(startIndex: 0, length: maxWidth);
        }

        public static List<string> Reflow(this IEnumerable<string> strings, int maxLength, int indent = 0) {
            var reflowed = new List<string>();

            foreach (var s in strings) {
                if (s.Length == 0) {
                    reflowed.Add(s);
                    continue;
                }

                var words = s.Split(' ').ToList();

                var line    = new StringBuilder();
                var newLine = true;

                while (words.Any()) {
                    var word = words[index: 0];
                    if (word.Length > maxLength) {
                        // word longer than maxLength means we have to split it anyway
                        var split = maxLength - line.Length;
                        word            = word.Substring(startIndex: 0, length: split);
                        words[index: 0] = word.Substring(split);
                    }

                    if (line.Length + word.Length + 1 < maxLength) {
                        // word fits in line
                        if (!newLine) line.Append(" ");
                        newLine = false;
                        line.Append(word);
                        words.RemoveAt(index: 0);
                    } else {
                        // doesn't fit, return what we've got and start a new line
                        reflowed.Add(line.ToString());
                        line.Clear();
                        line.Append(new string(c: ' ', count: indent));
                        newLine = true;
                    }
                }

                reflowed.Add(line.ToString());
            }

            return reflowed;
        }

        public static string JoinedBy(this IEnumerable<string> strings, string separator) =>
            string.Join(separator, strings);
    }
}