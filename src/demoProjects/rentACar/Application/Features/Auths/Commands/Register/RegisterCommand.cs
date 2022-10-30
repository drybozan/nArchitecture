using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Register
{
    public class RegisterCommand:IRequest<RegisteredDto>
    {
        //register olacak kişinin bilgilerini tuttar
        public UserForRegisterDto UserForRegisterDto { get; set; }

        //refresh tokenda ip bazlı doğrulama ve yönetim süreci vardır.
        public string IpAddress { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
        {
            //register için kural sınıfım
            private readonly AuthBusinessRules _authBusinessRules;
            // user db operasyonları için
            private readonly IUserRepository _userRepository;

            //token oluşturmak için 
            private readonly IAuthService _authService;

            public RegisterCommandHandler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, IAuthService authService)
            {
                _authBusinessRules = authBusinessRules;
                _userRepository = userRepository;
                _authService = authService;
            }

            public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                // kullanıcı daha önce kayıtlı ise tekrar kayıt edilemez email kontrol!
                await _authBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.UserForRegisterDto.Email);

                byte[] passwordHash, passwordSalt;

                //password hash oluştur.
                //out keywordu set eder alınan değeri
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

                //oluşan dto yu entitye dönüştür.(mapper kullanılmayan verisyonu)
                User newUser = new()
                {
                    Email = request.UserForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    Status = true
                };

                //oluşan entityi database kaydet
                User createdUser = await _userRepository.AddAsync(newUser);

                //yeni kullanıcı için bir accestoken üretmem gerekiyor
                AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);

                //yeni kullanıcı için bir refreshtoken üretmem gerekiyor
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAddress);
                //bunları veritabanına yollamak gerekiyor
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                // bir registerdto dönmem gerekiyor
                RegisteredDto registeredDto = new()
                {
                    RefreshToken = addedRefreshToken,
                    AccessToken = createdAccessToken,
                };

                return registeredDto;

            }
        }
    }
}