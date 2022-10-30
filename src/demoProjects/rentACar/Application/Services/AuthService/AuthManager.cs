using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public class AuthManager : IAuthService
    {
        //oluşturalacak json web tokenda kullanıcı rollerini dahil etmek için
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        // token oluşturmak için
        private readonly ITokenHelper _tokenHelper;

        //refresh token oluşturmak için
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthManager(IUserOperationClaimRepository userOperationClaimRepository, ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _tokenHelper = tokenHelper;
            _refreshTokenRepository = refreshTokenRepository;
        }

        //veritabanına refresh edilmiş token kaydedilir.
        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
            return addedRefreshToken;
        }
        // refresh token oluşturur.
        public async Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
        {
            RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
            return await Task.FromResult(refreshToken);
        }

        // kullanıcı için token oluşturur.
        public async Task<AccessToken> CreateAccessToken(User user)
        {
            // git userları çek aynı zamanda rolleri de dahil et 
            IPaginate<UserOperationClaim> userOperationClaims =
               await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id,
                                                                include: u => u.Include(u => u.OperationClaim)
               );

            //operationclaimleri gelen dataların içinden ayırt et isim ve idleri ile birlikte listele.
            IList<OperationClaim> operationClaims =
                userOperationClaims.Items.Select(u => new OperationClaim
                { Id = u.OperationClaim.Id, Name = u.OperationClaim.Name }).ToList();

            // user ve operation claimler ile birlikte token üret
            AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
            return accessToken;
        }

     
    }
}