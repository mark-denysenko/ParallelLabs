using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lab4.SleepingBarber
{
    public class Barber
    {
        public bool IsSleeping { get; private set; } = false;

        private readonly BarberShop barberShop;

        private readonly AutoResetEvent sleepingBarber = new AutoResetEvent(false);


        public Barber(BarberShop barberShop)
        {
            this.barberShop = barberShop;
        }

        public void Work()
        {
            try
            {
                while (true)
                {
                    var nextCustomer = barberShop.GetCustomerFromQueue();

                    if (nextCustomer is null)
                    {
                        Sleep();
                    }
                    else
                    {
                        MakeHairCut(nextCustomer);
                    }
                }
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Barber was interrupted!");
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("Barber was aborted!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Barber exception: " + ex.Message);
            }
        }

        public void WakeUp()
        {
            sleepingBarber.Set();
        }

        private void Sleep()
        {
            IsSleeping = true;
            Console.WriteLine("Barber start sleep");
            sleepingBarber.WaitOne();
            IsSleeping = false;
        }

        private void MakeHairCut(Customer customer)
        {
            const int haircutTime = 1500;

            Console.WriteLine("Making haircut");
            Thread.Sleep(haircutTime);
        }
    }
}
