namespace LE.Common.Exceptions
{
    public class ReservedFieldException : Exception
    {
        public ReservedFieldException()
        {
        }

        public ReservedFieldException(string? message) : base(message)
        {
        }

        public ReservedFieldException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
