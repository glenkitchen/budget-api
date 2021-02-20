namespace Application.Queries
{
    public class BaseQuery
    {
        public string Filters { get; set; }
        public string Orders { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
    
    public class IdQuery
    {
        public int Id { get; set; }
    }
}
