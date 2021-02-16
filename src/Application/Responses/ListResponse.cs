using System.Collections.Generic;

namespace Application.Responses
{
    public class ListResponse<T> where T : class 
    {
        public int Count { get; set; }
        public List<T> Data { get; set; }
    }
}
