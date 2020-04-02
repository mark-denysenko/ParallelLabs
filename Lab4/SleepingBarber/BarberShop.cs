using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab4.SleepingBarber
{
    public class BarberShop
    {
        private readonly int WaitingChairs;

        private readonly ConcurrentQueue<Customer> customersQueue = new ConcurrentQueue<Customer>();
        private readonly Barber barber;

        public BarberShop(int totalWaitingChairs)
        {
            WaitingChairs = totalWaitingChairs;
            barber = new Barber(this);
        }

        public Customer GetCustomerFromQueue()
        {
            if (customersQueue.TryDequeue(out var nextInQueue))
            {
                return nextInQueue;
            }

            return null;
        }

        public void EnterCustomer(Customer newCustomer)
        {
            if (customersQueue.Count >= WaitingChairs)
            {
                Console.WriteLine("Customer didn't get place in queue");
                return;
            }

            if (barber.IsSleeping)
            {
                Console.WriteLine("Wake up, barber!");
                customersQueue.Enqueue(newCustomer);
                barber.WakeUp();
            }
            else
            {
                Console.WriteLine("Added to queue!");
                customersQueue.Enqueue(newCustomer);
            }
        }

        public void StartWork()
        {
            var barberThread = new Thread(barber.Work)
            {
                IsBackground = true
            };

            barberThread.Start();


            Enumerable.Range(0, 50)
                .AsParallel()
                .AsUnordered()
                .WithDegreeOfParallelism(3)
                .ForAll(index =>
                {
                    Thread.Sleep(400);
                    if (index % 2 == 0)
                        Thread.Sleep(800);

                    EnterCustomer(new Customer());
                });
        }
    }
}
