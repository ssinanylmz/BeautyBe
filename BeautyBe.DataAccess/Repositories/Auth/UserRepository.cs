using BeautyBe.Core.Models.Auth;
using BeautyBe.Core.Repositories.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeautyBe.DataAccess.Repositories.Auth
{
    public class UserRepository : Service<User>, IUserRepository
    {
        private CoreDbContext _coreDbContext => _context;
        public UserRepository(CoreDbContext context) : base(context)
        {

        }
        public Task<User> GetUserWithRolesByIdAsync(int UserId)
        {
            return _coreDbContext.Users.FirstOrDefaultAsync(x => x.UserId == UserId);
        }
    }
}
