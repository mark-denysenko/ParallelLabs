using Lab4.ProducerConsumer;
using Lab4.ReaderWriterBlock;
using Lab4.SleepingBarber;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            // lab 1
            //ProducerConsumer();

            // lab 2
            //ReadWritersBlock();

            // lab 3
            DiningPhilosophers();

            // lab 4
            SleepingBarber();

            Console.ReadKey();
        }

        static void ProducerConsumer()
        {
            var queue = new CustomQueue<string>(1000);
            //var queue = new CustomQueueResetEvent<string>(1000);

            var producers = Enumerable
                .Range(0, 4)
                .Select(i => new Producer<string>(queue, () => $"{i} producer - {DateTime.Now.ToString()}"))
                .Select(producer => new Thread(producer.StartProduce))
                .ToList();
            var consumers = Enumerable
                .Range(0, 2)
                .Select(i => new Consumer<string>(queue))
                .Select(producer => new Thread(producer.StartConsume))
                .ToList();

            producers.ForEach(t => t.Start());
            consumers.ForEach(t => t.Start());

            Thread.Sleep(5000);

            producers.ForEach(p => p.Interrupt());
            consumers.ForEach(c => c.Interrupt());

        }

        private static void ReadWritersBlock()
        {
            var container = new ReadWriteContainer<string>("Initial");

            // Readers generator
            Task.Factory.StartNew(() =>
            {
                Enumerable.Range(0, 1000)
                    .AsParallel()
                    .AsUnordered()
                    .WithDegreeOfParallelism(5)
                    .ForAll(i =>
                    {
                        Thread.Sleep(30);
                        var reader = new Reader<string>(container);
                        reader.Read();
                    });
            });

            // Writers generator
            Task.Factory.StartNew(() =>
            {
                Enumerable.Range(0, 100)
                    .AsParallel()
                    .AsUnordered()
                    .WithDegreeOfParallelism(2)
                    .ForAll(i =>
                    {
                        Thread.Sleep(80);
                        var writer = new Writer<string>(container);
                        writer.Write($"{i} write - {DateTime.Now}");
                    });
            });

            Thread.Sleep(5000);
        }

        private static void DiningPhilosophers()
        {

        }


        private static void SleepingBarber()
        {
            var barberShop = new BarberShop(3);
            barberShop.StartWork();

            foreach(var customer in Enumerable.Range(0, 50))
            {
                Thread.Sleep(400);
                if (customer % 2 == 0)
                    Thread.Sleep(800);

                barberShop.EnterCustomer(new Customer());
            }
                
        }
    }
}
