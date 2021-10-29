using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace LoginExample.Data.Impl
{
    public interface IUserData
    { 
        Task<IList<User>> GetUsersAsync();
            Task<User> AddUserAsync(User user);
            Task RemoveUserAsync(int Id);
            Task<User> UpdateAsync(User user);
            User Get(int id);
            Task<User> ValidateUser(string userName, string password);
    }
}