using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    // hem register hem login için kullanılacak ortak servis
    public interface IAuthService
    {
        //giriş için erişim tokenı oluşturur
        public Task<AccessToken> CreateAccessToken(User user);

        //refresh token oluşturmak için
        public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress);

        //refresh tokenı doğrulama yapmak,karşılaştırma yap için ve veritabanına kaydetmek için
        public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
    }
}
