namespace PersonDirectory.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public object Key { get; }

        public NotFoundException(object key)
        {
            Key = key;
        }
    }
}