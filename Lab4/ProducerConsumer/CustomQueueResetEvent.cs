using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;

namespace Lab4.ProducerConsumer
{
    public class CustomQueueResetEvent<T> : IProducerConsumerCollection<T>
    {
        public int MaxSize { get; private set; }

        private Queue<T> queue = new Queue<T>();

        private AutoResetEvent lockForEmpty = new AutoResetEvent(false);
        private AutoResetEvent lockForFull = new AutoResetEvent(true);

        public CustomQueueResetEvent(int maxSize)
        {
            MaxSize = maxSize;
        }

        public bool TryAdd(T item)
        {
            Monitor.Enter(queue);
            try
            {
                if (Count == MaxSize)
                {
                    //lockForFull.Reset();
                    lockForFull.WaitOne();
                }


                queue.Enqueue(item);

                lockForEmpty.Set();
            }
            finally
            {
                Monitor.Exit(queue);
            }

            return true;
        }

        public bool TryTake([MaybeNullWhen(false)] out T item)
        {
            Monitor.Enter(queue);
            try
            {
                if (Count == 0)
                {
                    //lockForEmpty.Reset();
                    lockForEmpty.WaitOne();
                }


                item = queue.Dequeue();

                lockForFull.Set();
            }
            finally
            {
                Monitor.Exit(queue);
            }

            return true;
        }

        ~CustomQueueResetEvent()
        {
            lockForEmpty.Dispose();
            lockForFull.Dispose();
        }

        public int Count => queue.Count;

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public void CopyTo(T[] array, int index)
        {
            throw new NotImplementedException();
        }

        public T[] ToArray()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
