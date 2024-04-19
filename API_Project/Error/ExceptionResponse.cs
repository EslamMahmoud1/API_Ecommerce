namespace API_Project.Error
{
    public class ExceptionResponse : ErrorResponseBody
    {
        public string? Details { get; set; }
        public ExceptionResponse(int statusCode, string? errorMessage = null ,string? details =null) : base(statusCode, errorMessage)
        {
            Details = details;
        }
    }
}
