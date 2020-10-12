using Fabrit.Heroes.Data.Business.Authentication;
using Fabrit.Heroes.Data.Business.User;
using Fabrit.Heroes.Data.Entities.User;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services.Contracts
{
    public interface IUserService
    {
        Task CreateUser(RegisterDto registerDto);
        Task CreateHeroUser(User user);
        Task ActivateAccount(string token);
        Task ResetPassword(string email, bool isForgotten, string password = null, string newPassword = null);
        IAsyncEnumerable<User> GetAll();
        Task<User> FindById(int id);
        Task UpdateUser(UserDto userDto);
        Task DeleteUserById(int id);
    }
}
