namespace PersonDirectory.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public string ValidationKey { get; }

        public object Key { get; }

        public NotFoundException(string validationKey, object key)
        {
            ValidationKey = validationKey;
            Key = key;
        }
    }
}