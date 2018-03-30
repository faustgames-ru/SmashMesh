using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Core
{
    public interface IPoolOnObtain
    {
        void PoolOnObtain();
    }

    public interface IPoolOnReturn
    {
        void PoolOnReturn();
    }

    public class Pool<T> where T: class, new()
    {
        public void Reserve(int reserve)
        {
            for (var i = 0; i < reserve; i++)
            {
                _entries.Push(new T());
            }
        } 

        public T Obtain()
        {
            T result = null;
            if (_entries.Count == 0)
            {
                result = new T();
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
