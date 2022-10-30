using Core.Security.Entities;

namespace Core.Security.JWT;

public interface ITokenHelper
{
    //jwt helper içinde gövdeleri mevcut
    AccessToken CreateToken(User user, IList<OperationClaim> operationClaims);

    RefreshToken CreateRefreshToken(User user, string ipAddress);
}