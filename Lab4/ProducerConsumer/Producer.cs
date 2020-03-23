using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lab4.ProducerConsumer
{
    public class Producer<T>
    {
        protected IProducerConsumerCollection<T> collection;
        protected Func<T> generator;

        public Producer(IProducerConsumerCollection<T> collection, Func<T> generator)
        {
            this.collection = collection;
            this.generator = generator;
        }

        public void StartProduce()
        {
            while (true)
            {
                var produced = generator();
                Console.WriteLine($"Produced by {Thread.CurrentThread.ManagedThreadId} - {produced}");
                try
                {
                    collection.TryAdd(produced);
                    Thread.Sleep(500);
                }
                catch (ThreadInterruptedException)
                {
                    Console.WriteLine("Producer '{0}' interrupted.", Thread.CurrentThread.ManagedThreadId);
                    break;
                }
            }
        }
    }
}
