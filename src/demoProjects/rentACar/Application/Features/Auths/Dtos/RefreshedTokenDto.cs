using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Dtos
{
    public class RefreshedTokenDto
    {
        //token ve kullanım süresi mevcut
        public AccessToken AccessToken { get; set; }

        // token yenileme için
        public RefreshToken RefreshToken { get; set; }
    }
}
