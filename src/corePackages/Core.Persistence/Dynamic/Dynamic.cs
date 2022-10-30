namespace Core.Persistence.Dynamic;

public class Dynamic
{
    // sıralama için
    public IEnumerable<Sort>? Sort { get; set; }
    //filtreleme için
    public Filter? Filter { get; set; }

    public Dynamic()
    {
    }

    public Dynamic(IEnumerable<Sort>? sort, Filter? filter)
    {
        Sort = sort;
        Filter = filter;
    }
}