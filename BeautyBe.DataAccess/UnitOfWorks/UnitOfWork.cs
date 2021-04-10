using BeautyBe.Core.Repositories.Auth;
using BeautyBe.Core.UnitOfWorks;
using BeautyBe.DataAccess.Repositories.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeautyBe.DataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoreDbContext _context;
        private UserRepository _userRepository;
        public IUserRepository User => _userRepository = _userRepository ?? new UserRepository(_context);

        public UnitOfWork(CoreDbContext coreDbContext)
        {
            _context = coreDbContext; 
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
