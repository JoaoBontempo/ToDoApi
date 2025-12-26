namespace Shared.Exceptions
{
    public class AppEntityNotFoundException : Exception
    {
        public AppEntityNotFoundException()
        {
        }

        public AppEntityNotFoundException(string? message) : base(message)
        {
        }
    }
}
