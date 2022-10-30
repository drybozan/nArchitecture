namespace Core.Persistence.Dynamic;

public class Sort
{
    // sıralama yapacağı alan ismini tutmak için
    public string Field { get; set; }
    // artan sırada mı azalan sırada mı sıralasın ? asc desc değerlerini bildirmek için
    public string Dir { get; set; }

    public Sort()
    {
    }

    public Sort(string field, string dir)
    {
        Field = field;
        Dir = dir;
    }
}