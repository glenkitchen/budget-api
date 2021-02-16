using System.Collections.Generic;

namespace Application.Responses
{
    public class OperationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object OperationId { get; set; }
        public List<string> ValidationErrors { get; set; }

        public OperationResponse()
        {
            Success = true;
        }
    }

    public class OperationResponse<T> : OperationResponse
    {
        public T Data { get; set; }        
    }
}
