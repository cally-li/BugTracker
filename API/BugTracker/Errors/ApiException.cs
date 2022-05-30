//used in Exception Middleware
namespace BugTracker.Errors
{
    public class ApiException
    {
        //string and details default to null if none passed in
        public ApiException(int statusCode, string? message = null, string? details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode{ get; set; }

        public string Message{ get; set; }
        public string Details { get; set; }
    }
}
