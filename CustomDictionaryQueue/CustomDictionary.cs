// ******************************************************************************************************************
//  Custom Dictionary that uses a queue to restrict the size of the dictionary.
//  Copyright(C) 2018  James LoForti
//  Contact Info: jamesloforti@gmail.com
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.If not, see<https://www.gnu.org/licenses/>.
//									     ____.           .____             _____  _______   
//									    |    |           |    |    ____   /  |  | \   _  \  
//									    |    |   ______  |    |   /  _ \ /   |  |_/  /_\  \ 
//									/\__|    |  /_____/  |    |__(  <_> )    ^   /\  \_/   \
//									\________|           |_______ \____/\____   |  \_____  /
//									                             \/          |__|        \/ 
//
// ******************************************************************************************************************
//
using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomDictionaryQueue
{
    class CustomDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        //Data Members:
        Dictionary<TKey, TValue> _dictionary;
        Queue<TKey> _queue;
        int _size;

        public CustomDictionary(int size)
        {
            _size = size;
            _dictionary = new Dictionary<TKey, TValue>(size + 1);
            _queue = new Queue<TKey>(size);
        }

        //*************************BELOW METHODS ARE REQUIRED BY IDICTIONARY**************************
        public TValue this[TKey key] { get => _dictionary[key]; set => throw new NotImplementedException(); }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);

            if (_queue.Count == _size)
            {
                _dictionary.Remove(_queue.Dequeue());
            }

            _queue.Enqueue(key);
        } // end method Add()

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            foreach (TKey item in _queue)
            {
                if (_dictionary.Comparer.Equals(item, key))
                {
                    return true;
                }
            } // end foreach

            return false;
        } // end method ContainsKey()

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var e in _dictionary)
            {
                yield return e;
            }
        } // end method GetEnumerator()

        public bool Remove(TKey key)
        {
            if (_dictionary.Remove(key))
            {
                Queue<TKey> newQueue = new Queue<TKey>(_size);

                foreach (TKey item in _queue)
                {
                    if (!_dictionary.Comparer.Equals(item, key))
                    {
                        newQueue.Enqueue(item);
                    }
                } // end foreach

                _queue = newQueue;
                return true;
            } // end if

            return false;
        } // end method Remove()

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    } // end class CustomDictionary
} // end namespace CustomDictionaryQueue
