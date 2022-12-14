using Core.Persistence.Repositories;

namespace Core.Security.Entities;


// kullanıcnın rollerini belirten class
public class OperationClaim : Entity
{
    public string Name { get; set; }

    public OperationClaim()
    {
    }

    public OperationClaim(int id, string name) : base(id)
    {
        Name = name;
    }
}