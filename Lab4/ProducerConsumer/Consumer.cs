using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lab4.ProducerConsumer
{
    public class Consumer<T>
    {
        protected IProducerConsumerCollection<T> collection;

        public Consumer(IProducerConsumerCollection<T> collection)
        {
            this.collection = collection;
        }

        public void StartConsume()
        {
            while(true)
            {
                try
                {
                    collection.TryTake(out var item);
                    Console.WriteLine($"Consumed by {Thread.CurrentThread.ManagedThreadId} - " + item);
                }
                catch (ThreadInterruptedException)
                {
                    Console.WriteLine("Consumer '{0}' interrupted.", Thread.CurrentThread.ManagedThreadId);
                    break;
                }

            }
        }
    }
}
