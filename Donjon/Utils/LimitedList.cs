using System.Collections;
using System.Collections.Generic;

namespace Donjon.Utils {
    public class LimitedList<T> : IEnumerable<T> {
        private readonly List<T> list = new List<T>();

        public LimitedList(int capacity) => Capacity = capacity < 0 ? 0 : capacity;

        public int  Capacity { get; }
        public int  Count    => list.Count;
        public bool IsFull   => list.Count >= Capacity;

        public IEnumerator<T> GetEnumerator() {
            foreach (var item in list) yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Add(T item) {
            if (IsFull) return false;
            list.Add(item);
            return true;
        }

        public bool Remove(T item)      => list.Remove(item);
        public void RemoveAt(int index) => list.RemoveAt(index);
        public bool Contains(T item)    => list.Contains(item);
    }
}