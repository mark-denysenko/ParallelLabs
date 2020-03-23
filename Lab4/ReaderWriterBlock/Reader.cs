using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lab4.ReaderWriterBlock
{
    public class Reader<T>
    {
        private IReadable<T> readableSource;

        public Reader(IReadable<T> source)
        {
            readableSource = source;
        }

        public void Read()
        {
            try
            {
                var value = readableSource.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
