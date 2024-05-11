namespace Shortener.Data.Validation
{
    [Serializable]
    public class DatabaseDoesntContainException : Exception
    {
        public DatabaseDoesntContainException() : base() { }
        public DatabaseDoesntContainException(string message) : base(message) { }
        public DatabaseDoesntContainException(string message, Exception innerException) : base(message, innerException) { }
    }
}
