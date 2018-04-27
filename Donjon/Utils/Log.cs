using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Donjon.Utils {
    public class Log : IEnumerable<string> {
        private readonly List<string> log = new List<string>();
        private          int          oldMessages;
        public           int          Count => log.Count;

        public IEnumerable<string> Old => log.Take(oldMessages);
        public IEnumerable<string> New => log.Skip(oldMessages);

        public IEnumerator<string> GetEnumerator() => log.GetEnumerator();
        IEnumerator IEnumerable.   GetEnumerator() => GetEnumerator();

        public void Add(string item) => log.Add(item);
        public void Clear()          => log.Clear();
        public void Archive()        => oldMessages = Count;
    }
}