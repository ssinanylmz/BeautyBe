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

        public async Task<User> GetUserWithRolesByIdAsync(int UserId)
        {
            return await _unitOfWork.User.GetUserWithRolesByIdAsync(UserId);
        }
    }
}
