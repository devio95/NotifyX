namespace Application.Interfaces.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException() : base(string.Empty) { }
        public ConflictException(string message) : base(message) { }
    }
}
