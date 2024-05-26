namespace Caber.Models
{
    public class ErrorModel(string message, int code)
    {

        public string Message = message;
        public int StatusCode = code;
    }
}