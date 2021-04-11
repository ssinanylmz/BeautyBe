using BeautyBe.Core.Models.Auth;
using BeautyBe.Core.Repositories;
using BeautyBe.Core.Services.Auth;
using BeautyBe.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BeautyBe.Service.Services.Auth
{
    public class UserService : Service<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IRepository<User> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<User> LoginGetUserAsync(string userName, string password, string email)
        {
            return await _unitOfWork.User.LoginGetUserAsync(userName, password, email);
        }
    }
}
