using BeautyBe.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeautyBe.Core.Services.Auth
{
    public interface IUserService : IService<User>
    {
        Task<User> LoginGetUserAsync(string userName, string password, string email); 
    }
}
