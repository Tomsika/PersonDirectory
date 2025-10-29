namespace PersonDirectory.Application.Exceptions
{
    public class AlreadyExsistExeption : Exception
    {
        public string Key { get; }

        public AlreadyExsistExeption(string key, string message)
            : base(message)
        {
            Key = key;
        }
    }
}