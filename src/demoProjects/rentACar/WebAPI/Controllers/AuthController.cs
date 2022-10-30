using Application.Features.Auths.Commands.Register;
using Application.Features.Auths.Dtos;
using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registerCommand = new()
            {
                UserForRegisterDto = userForRegisterDto,
                IpAddress = GetIpAddress() //Basecontrollerdan miras alınan metot
            };

            //register olan kullanıcıyı resulttan al 
            RegisteredDto result = await Mediator.Send(registerCommand);

            //oluşan refresh tokenı cookieye yerleştirip yolluyoruz.
            SetRefreshTokenToCookie(result.RefreshToken);

            return Created("",result.AccessToken);
        }

        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true ,Expires = DateTime.Now.AddDays(7)};

            //refreshToken adında tokenımızı cookiye dahil et
            Response.Cookies.Append("refreshToken",refreshToken.Token, cookieOptions);
        }

    }
}
