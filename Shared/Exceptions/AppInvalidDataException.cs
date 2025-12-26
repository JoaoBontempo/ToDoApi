namespace Shared.Exceptions
{
    public class AppInvalidDataException : Exception
    {
        public AppInvalidDataException()
        {
        }

        public AppInvalidDataException(string? message) : base(message)
        {
        }
    }
}
