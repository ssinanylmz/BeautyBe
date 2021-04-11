using AutoMapper;
using BeautyBe.API.Dtos.Auth;
using BeautyBe.Core.Models.Auth;
using BeautyBe.Core.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BeautyBe.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public LoginController(IUserService userService,ITokenService tokenService,IMapper mapper,IConfiguration configuration)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginDto loginInfo)
        {
            if(loginInfo != null && (loginInfo.UserName !=null || loginInfo.Email != null) && loginInfo.PasswordCrypt!=null)
            {
                var user =await GetUser(loginInfo.UserName, loginInfo.PasswordCrypt, loginInfo.Email);
               
                if (user != null)
                {
                  
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                        new Claim("UserName",user.UserName),
                        new Claim("Email",user.Email),
                        new Claim("Password",user.PasswordCrypt)
                    };
                    
                    var accessToken = _tokenService.GenerateAccessToken(claims);
                    var refreshToken = _tokenService.GenerateRefreshToken();
                    var userFull = await _userService.SingleOrDefaultAsync(x => x.UserName == user.UserName);
                    userFull.RefreshToken = refreshToken;
                    userFull.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                    _ = _userService.Update(_mapper.Map<User>(userFull));
                    return new OkObjectResult(new 
                    { accessToken = accessToken,
                      refreshToken = refreshToken 
                    });
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }

            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> Refresh(string accessToken,string refreshToken)
        {
            if (refreshToken == null)
            {
                return BadRequest("Invalid client request");
            }
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var userName = principal.Identity.Name; //this is mapped to the Name claim by default
            var user = await _userService.SingleOrDefaultAsync(x => x.UserName == userName);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid client request");
            }
            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
             _userService.Update(_mapper.Map<User>(user));
            return new ObjectResult(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        [HttpGet("GetUser")]

        [Authorize]
        public async Task<LoginDto> GetUser(string userName, string password, string email)
        {
            var users = await _userService.LoginGetUserAsync(userName, password, email);
            LoginDto userDto = _mapper.Map<LoginDto>(users);
            return userDto;
        }
        
    }
}
