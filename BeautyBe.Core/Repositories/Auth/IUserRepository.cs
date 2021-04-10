using BeautyBe.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeautyBe.Core.Repositories.Auth
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> GetUserWithRolesByIdAsync(int UserId);
    }
}
