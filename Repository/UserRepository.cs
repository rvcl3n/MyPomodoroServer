using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await FindAll()
                .OrderBy(p => p.Email)
                .ToListAsync();
        }

        public async Task<User> GetUserById(Guid userId)
        {
            return await FindByCondition(user => user.Id.Equals(userId))
                    .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByExternalId(string externalId)
        {
            return await FindByCondition(user => user.ExternalId.Equals(externalId))
                    .FirstOrDefaultAsync();
        }

        public async Task CreateUser(User user)
        {
            await Create(user);
        }

        public async Task UpdateUser(User dbUser, User user)
        {
            await Update(user);
        }

        public async Task DeleteUser(User user)
        {
            await Delete(user);
        }
    }
}
