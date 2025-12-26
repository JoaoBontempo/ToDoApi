namespace Presentation.Dto.Response
{
    public class AppDefaultResponse
    {
        public AppDefaultResponse(bool success) { 
            Success = success;
        }

        public AppDefaultResponse(object? data)
        {
            Data = data;
            Success = true;
        }

        public AppDefaultResponse(string error)
        {
            Error = error;
            Success = false;
        }

        public AppDefaultResponse(Exception exception) : this(exception.Message) { }

        public bool Success { get; set; }
        public string? Error { get; set; }
        public object? Data { get; set; }
    }
}
