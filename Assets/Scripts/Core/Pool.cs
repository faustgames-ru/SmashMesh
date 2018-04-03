using System.Collections.Generic;
using System;

namespace Core
{
    public class Pool<T> where T: class
    {
        public void Reserve(int reserve, Func<T> constructor)
        {
            T entry = null;
            for (var i = 0; i < reserve; i++)
            {
                entry = constructor();
                _entries.Push(entry);
            }
        }

        public void Reserve(IEnumerable<T> entries)
        {
            foreach (var entry in entries)
            {
                _entries.Push(entry);
            }
        }

        public T Obtain(Func<T> constructor)
        {
            T result = null;
            if (_entries.Count == 0)
            {
                if (constructor == null)
                    return null;
                result = constructor();
            }
            else
            {
                result = _entries.Pop();
            }
            (result as IPoolOnObtain)?.PoolOnObtain();
            return result;
        }

        public void Return(T entry)
        {
            (entry as IPoolOnReturn)?.PoolOnReturn();
            _entries.Push(entry);
        }

        Stack<T> _entries = new Stack<T>();
    }
}
