﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lab4.SleepingBarber
{
    public class BarberShop
    {
        private readonly int WaitingChairs;

        private ConcurrentQueue<Customer> customersQueue = new ConcurrentQueue<Customer>();
        private Barber barber;

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
        }
    }
}
