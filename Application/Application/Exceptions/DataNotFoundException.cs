namespace Application.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException() : base(string.Empty) { }
        public DataNotFoundException(string message) : base(message) { }
    }
}