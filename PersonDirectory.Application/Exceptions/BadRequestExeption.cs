namespace PersonDirectory.Application.Exceptions
{
    public class BadRequestExeption : Exception
    {
        public object ReasonKey { get; }

        public BadRequestExeption(object reasoneKey)
        {
            ReasonKey = reasoneKey;
        }
    }
}