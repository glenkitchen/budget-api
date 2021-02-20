namespace Persistence.Paging
{
    internal enum Operator
    {
        Equals,
        NotEqual,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
        Like,
        Contains,
        NotContains
    }

    internal class Filter {
        public string Property { get; set; }
        public Operator Operator { get; set; }
        public string Value { get; set; }        
    }

    internal class Order {
        public string Property { get; set; }
        public bool IsAscending { get; set; }
    }
}
