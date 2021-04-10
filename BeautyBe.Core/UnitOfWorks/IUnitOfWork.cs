using BeautyBe.Core.Repositories.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeautyBe.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IUserRepository User {get; }
        Task CommitAsync();
        void Commit();
    }
}
