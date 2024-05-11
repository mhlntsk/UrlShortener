namespace Shortener.Business.Validation
{
    [Serializable]
    public class ShortenerException : Exception
    {
        public ShortenerException() : base() { }
        public ShortenerException(string message) : base(message) { }
        public ShortenerException(string message, Exception innerException) : base(message, innerException) { }
    }
}
