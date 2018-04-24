using System;
using System.Collections.Generic;

namespace Donjon.Utilities
{
    public class Log
    {
        private List<string> messages = new List<string>();

        public void Add(string message)
        {
            messages.Add(message);
        }

        internal IEnumerable<string> GetAll()
        {
            return messages;
        }

        internal void Clear() {
            messages.Clear();
        }

        internal IEnumerable<string> GetLast(int lines)
        {
            var length = messages.Count;
            int start = Math.Max(0, length - lines);
            for (int i = start; i < length; i++)
            {
                yield return messages[i];
            }
        }
    }
}