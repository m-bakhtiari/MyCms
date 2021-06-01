using MyCms.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MyCms.Domain.Interfaces
{
    public interface IUserRepository : IAsyncDisposable
    {
        #region Add Or Update User

        Task<int> AddUserAsync(User user);

        #endregion

        #region Get User

        Task<User> GetUserByUserId(int userId);

        #endregion

        #region Validations

        Task<User> LoginUser(string email, string password);

        #endregion

        #region Delete

        Task DeleteUser(int userId);

        #endregion
    }
}
