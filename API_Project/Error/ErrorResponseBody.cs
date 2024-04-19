
using Microsoft.AspNetCore.Http;

namespace API_Project.Error
{
    public class ErrorResponseBody
    {
        public ErrorResponseBody(int statusCode , string? errorMessage = null)
        {
            ErrorMessage = errorMessage ?? GetDefaultMessage(statusCode); 
            StatusCode = statusCode;
        }

        private string? GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                200 => "OK",
                404 => "Not Found" ,
                _ => "Something Went Wrong"
            };
        }
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
