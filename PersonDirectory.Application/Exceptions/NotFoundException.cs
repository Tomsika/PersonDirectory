namespace PersonDirectory.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public int Key { get; }

        public NotFoundException(string message, int key)
            : base(message)
        {
            Key = key;
        }
    }
}