using AutoMapper;
using BeautyBe.API.Dtos.Auth;
using BeautyBe.Core.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyBe.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public LoginController(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Login(string userName, string password, string email)
        {
            var users = await _userService.LoginGetUserAsync(userName, password, email);
            return Ok(_mapper.Map<LoginDto>(users));
        }
    }
}
