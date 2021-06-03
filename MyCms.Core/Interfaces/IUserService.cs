using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCms.Core.Services;
using MyCms.Core.ViewModels;
using MyCms.Domain.Dto;
using MyCms.Domain.Entities;

namespace MyCms.Core.Interfaces
{
    public interface IUserService
    {
        #region Add Or Update User

        Task<OpRes> RegisterUser(UserViewModel userViewModel);
        #endregion

        #region Get User

        Task<User> GetUserByUserId(int userId);
        Task<OpRes<PagedResult<UserDto, UserSearchItem>>> GetUserByPaging(UserSearchItem item);

        #endregion

        #region Validations

        Task<User> LoginUser(LoginViewModel user);

        Task<bool> IsUserInAdmin(int userId);

        #endregion

        #region Delete

        Task<OpRes> DeleteUser(int userId);

        #endregion
    }
}
