namespace PersonDirectory.Application.Exceptions
{
    public class AlreadyExsistExeption : Exception
    {
        public object Key { get; }

        public AlreadyExsistExeption(object key)
        {
            Key = key;
        }
    }
}