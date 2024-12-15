using System.Text;

namespace RestaurantManagement.Common.Exceptions
{
    [Serializable]
    public class InvalidInputException : Exception
    {
        public int Code { get; }
        public object? Details { get; }

        public InvalidInputException() { }
        public InvalidInputException(string message) : base(message) { }
        public InvalidInputException(string message, int code) : base(message)
        {
            Code = code;
        }
        public InvalidInputException(string message, int code, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }
        public InvalidInputException(string message, int code, object details) : base(message)
        {
            Code = code;
            Details = details;
        }
        public InvalidInputException(string message, int code, object details, Exception innerException) : base(message, innerException)
        {
            Code = code;
            Details = details;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Code: {Code.ToString()}. ");
            if (Details is not null) sb.AppendLine($"Details: {Details.ToString()}");
            sb.AppendLine(base.ToString());

            return sb.ToString();
        }
    }
}
