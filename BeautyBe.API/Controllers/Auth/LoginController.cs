using AutoMapper;
using BeautyBe.API.Dtos.Auth;
using BeautyBe.Core.Services.Auth;
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
        private readonly IMapper _mapper;
        public LoginController(IUserService userService,IMapper mapper,IConfiguration configuration)
        {
            _userService = userService;
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
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires:DateTime.Now.AddMinutes(20),
                        signingCredentials:signIn);
                    var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
                    var refreshToken = new JwtSecurityTokenHandler().WriteToken(token);
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
        [HttpGet]
        public async Task<LoginDto> GetUser(string userName, string password, string email)
        {
            var users = await _userService.LoginGetUserAsync(userName, password, email);
            LoginDto userDto = _mapper.Map<LoginDto>(users);
            return userDto;
        }
    }
}
