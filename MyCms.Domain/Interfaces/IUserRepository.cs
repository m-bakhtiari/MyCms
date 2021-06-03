using MyCms.Domain.Entities;
using System;
using System.Threading.Tasks;
using MyCms.Domain.Dto;

namespace MyCms.Domain.Interfaces
{
    public interface IUserRepository : IAsyncDisposable
    {
        #region Add Or Update User

        Task<int> AddUserAsync(User user);

        #endregion

        #region Get User

        Task<User> GetUserByUserId(int userId);

        /// <summary>
        /// get users by paging and search in items 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<PagedResult<UserDto, UserSearchItem>> GetUserByPaging(UserSearchItem item);

        #endregion

        #region Validations

        Task<User> LoginUser(string email, string password);
        Task<bool> IsEmailExist(string email);

        #endregion

        #region Delete

        Task DeleteUser(int userId);

        #endregion
    }
}
