using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donjon.Utilities
{
    public class LimitedList<T> : IEnumerable<T>
    {
        private List<T> list;

        public int Capacity { get; }
        public bool IsFull => list.Count >= Capacity;
        
        public int Count => list.Count;


        public LimitedList(int capacity)
        {
            Capacity = capacity;
            list = new List<T>(capacity);
        }

        public bool Add(T item)
        {
            if (IsFull) return false;
            list.Add(item);
            return true;
        }
        
        public bool Remove(T item) => list.Remove(item);

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in list)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
