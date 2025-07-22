namespace Application.Exceptions
{
    public class DataValidationException : Exception
    {
        public DataValidationException() : base(string.Empty) { }
        public DataValidationException(string message) : base(message) { }
    }
}
