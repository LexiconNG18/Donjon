using System.Collections;
using System.Collections.Generic;

namespace Donjon.Utils
{
    public class LimitedList<T> : IEnumerable<T>
    {
        private readonly List<T> list = new List<T>();

        public LimitedList(int capacity)
        {
            Capacity = capacity < 0 ? 0 : capacity;
        }

        public int Capacity { get; }
        public int Count => list.Count;

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in list) yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Add(T item)
        {
            if (list.Count >= Capacity) return false;
            list.Add(item);
            return true;
        }

        public bool Remove(T item)
        {
            return list.Remove(item);
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public bool IsFull()
        {
            return list.Count >= Capacity;
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }
    }
}