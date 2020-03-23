using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lab4.ReaderWriterBlock
{
    public class Writer<T>
    {
        private IWritable<T> writableSource;

        public Writer(IWritable<T> source)
        {
            writableSource = source;
        }

        public void Write(T value)
        {
            try
            {
                writableSource.Write(value);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
