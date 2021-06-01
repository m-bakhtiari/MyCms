using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCms.Core.ViewModels;
using MyCms.Domain.Entities;

namespace MyCms.Core.Interfaces
{
    public interface IUserService
    {
        #region Add Or Update User

        Task<OpRes> AddUserAsync(UserViewModel userViewModel);

        Task<OpRes> AddAdminAsync(UserViewModel userViewModel);
        #endregion

        #region Get User

        Task<User> GetUserByUserId(int userId);

        #endregion

        #region Validations

        Task<User> LoginUser(UserViewModel user);

        Task<bool> IsUserInAdmin(int userId);

        #endregion

        #region Delete

        Task<OpRes> DeleteUser(int userId);

        #endregion
    }
}
