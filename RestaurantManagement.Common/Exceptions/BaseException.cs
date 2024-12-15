namespace RestaurantManagement.Common.Exceptions
{
    [Serializable]
    public class BaseException : Exception
    {
        public int StatusCode { get; }

        public BaseException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public BaseException(int statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
