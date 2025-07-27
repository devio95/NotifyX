namespace Application.Interfaces.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base(string.Empty) { }

        public ForbiddenException(string message) : base(message) { }
    }
}
