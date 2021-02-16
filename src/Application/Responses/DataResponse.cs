namespace Application.Responses
{
    public class DataResponse<T> where T : class
    {
        public T Data { get; set; }
    }
}
