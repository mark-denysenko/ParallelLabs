using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Collections;
using System.Threading;

namespace Lab4.ProducerConsumer
{
    // ConcurrentQueue<T>
    // BlockingCollection
    // https://stackoverflow.com/questions/530211/creating-a-blocking-queuet-in-net

    // ManualResetEvent/AutoResetEvent, ReaderWriterLockSlim 
    public class CustomQueue<T>: IProducerConsumerCollection<T>
    {
        public int MaxSize { get; private set; }

        private Queue<T> queue = new Queue<T>();

        public CustomQueue(int maxSize)
        {
            MaxSize = maxSize;
        }

        public bool TryAdd(T item)
        {
            Monitor.Enter(queue);
            try
            {
                while (Count == MaxSize)
                {
                    Monitor.Wait(queue);
                }

                queue.Enqueue(item);
                Monitor.Pulse(queue);
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
                while (Count == 0)
                {
                    Monitor.Wait(queue);
                }

                item = queue.Dequeue();
                Monitor.Pulse(queue);
            }
            finally
            {
                Monitor.Exit(queue);
            }

            return true;
        }

        public int Count => queue.Count;
        public bool IsSynchronized => true;
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
