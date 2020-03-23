using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lab4.ReaderWriterBlock
{
    public class ReadWriteContainer<T> : IReadable<T>, IWritable<T>
    {
        private T storedValue;
        private SemaphoreSlim semaphore = new SemaphoreSlim(1);
        private Mutex mutex = new Mutex();
        private int readers = 0;
        private int writers = 0;

        public ReadWriteContainer() : this(default) { }

        public ReadWriteContainer(T initValue)
        {
            storedValue = initValue;
        }

        public T Read()
        {
            semaphore.Wait();
            var returnedValue = storedValue;
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} read - {returnedValue}");

            semaphore.Release();

            return returnedValue;
        }

        public void Write(T value)
        {
            // Writer wants to enter the critical section
            mutex.WaitOne();

            // there is atleast one writer in the critical section
            // this ensure no reader can enter if there is even one writer
            // thus we give preference to writer here
            if (writers == 0)
            {
                semaphore.Wait();
            }
            Interlocked.Increment(ref writers);

            // other writers can enter while this current writer is inside 
            // the critical section
            mutex.ReleaseMutex();

            // current writer performs writing here
            mutex.WaitOne();

            storedValue = value;
            Console.WriteLine(value);


            Interlocked.Decrement(ref writers);

            // that is, no writer is left in the critical section,
            // reader can enter
            if (writers == 0)
            {
                semaphore.Release();
            }

            // writer leaves
            mutex.ReleaseMutex();
        }


        //public T Read()
        //{
        //    if (readers == 0)
        //    {
        //        semaphore.Wait();
        //    }

        //    Interlocked.Increment(ref readers);

        //    var returnedValue = storedValue;
        //    //Thread.Sleep(100);

        //    Interlocked.Decrement(ref readers);

        //    if (readers == 0)
        //    {
        //        semaphore.Release();
        //    }

        //    return returnedValue;
        //}
        //public void Write(T value)
        //{
        //    semaphore.Wait();
        //    storedValue = value;
        //    semaphore.Release();
        //}

        ~ReadWriteContainer()
        {
            mutex.Dispose();
        }
    }
}
