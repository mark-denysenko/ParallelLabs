namespace Lab4.ReaderWriterBlock
{
    public interface IWritable<T>
    {
        void Write(T value);
    }
}