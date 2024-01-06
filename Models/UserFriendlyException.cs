namespace PSP.Models
{
    public class UserFriendlyException : Exception
    {
        public int StatusCode { get; set; }

        public UserFriendlyException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
