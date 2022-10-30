namespace Core.Persistence.Dynamic;

public class Filter
{
    // hangi alana göre filtrelesin
    public string Field { get; set; }
    //hangi operatörle
    public string Operator { get; set; }
    // alanın hangi değerine göre filtrelesin
    public string? Value { get; set; }
    // and , or 
    public string? Logic { get; set; }
    public IEnumerable<Filter>? Filters { get; set; }

    public Filter()
    {
    }

    public Filter(string field, string @operator, string? value, string? logic, IEnumerable<Filter>? filters) : this()
    {
        Field = field;
        Operator = @operator;
        Value = value;
        Logic = logic;
        Filters = filters;
    }
}