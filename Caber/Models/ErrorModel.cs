namespace Caber.Models
{
    public class ErrorModel(string message, int code)
    {

        public string Message { get; set; } = message;
        public int StatusCode { get; set; } = code;
    }
}