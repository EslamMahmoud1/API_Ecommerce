namespace API_Project.Error
{
    public class ValidationResponse : ErrorResponseBody
    {
        public ValidationResponse() : base(401)
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; set; }
    }
}
