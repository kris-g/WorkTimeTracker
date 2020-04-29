using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KrisG.TimeTracker.Entities;

namespace KrisG.TimeTracker.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsername(string username);
        Task<IEnumerable<User>> GetAll();
        Task<bool> UsernameExists(string username);
        Task<User> AddAsync(User item);
    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        protected override string JsonPath => @".\Users.json";

        public async Task<User> GetByUsername(string username)
        {
            var all = await GetAll();
            return all.FirstOrDefault(x => x.Username == username);
        }

        public async Task<bool> UsernameExists(string username)
        {
            var all = await GetAll();
            return all.Any(x => x.Username == username);
        }
    }
}
