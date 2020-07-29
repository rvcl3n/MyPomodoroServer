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

        public void CreateUser(User user)
        {
            Create(user);
        }

        public void UpdateUser(User dbUser, User user)
        {
            Update(user);
        }

        public void DeleteUser(User user)
        {
            Delete(user);
        }
    }
}
