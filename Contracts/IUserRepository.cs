using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(Guid UserId);
        Task<User> GetUserByExternalId(string externalId);
        Task CreateUser(User user);
        Task UpdateUser(User dbUser, User user);
        Task DeleteUser(User user);
    }
}
