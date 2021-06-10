using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using MyCms.Core.Extensions;
using MyCms.Core.Interfaces;
using MyCms.Core.ViewModels;
using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;
using MyCms.Extensions.Consts;
using MyCms.Domain.Dto;

namespace MyCms.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISaveChangesRepository _saveChangesRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserService(IUserRepository userRepository, ISaveChangesRepository saveChangesRepository, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _saveChangesRepository = saveChangesRepository;
            _userRoleRepository = userRoleRepository;
        }


        public async Task<OpRes> RegisterUser(UserViewModel userViewModel)
        {
            var validate = await ValidateUserInfo(userViewModel);
            if (validate.IsNullOrWhiteSpace() == false)
            {
                return OpRes.BuildError(validate);
            }
            OpRes res;
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            if (userViewModel.IsAdmin)
                res = await AddAdminAsync(userViewModel);
            else
                res = await AddUserAsync(userViewModel);
            await _saveChangesRepository.SaveChangesAsync();
            scope.Complete();
            return res;
        }


        private async Task<OpRes> AddUserAsync(UserViewModel userViewModel)
        {
            var userId = await AddRawUser(userViewModel);
            var userRole = new UserRole(userId, Const.UserRoleId);
            await _userRoleRepository.AddUserRoleAsync(userRole);

            return OpRes.BuildSuccess();

        }

        public async Task<OpRes> AddAdminAsync(UserViewModel userViewModel)
        {
            var userId = await AddRawUser(userViewModel);
            var userRole = new UserRole(userId, Const.AdminRoleId);
            await _userRoleRepository.AddUserRoleAsync(userRole);

            return OpRes.BuildSuccess();
        }

        private async Task<int> AddRawUser(UserViewModel userViewModel)
        {
            var user = new User()
            {
                Email = userViewModel.Email.ToLowerInvariant(),
                FullName = userViewModel.FullName,
                Password = userViewModel.Password.EncodePasswordMd5(),
                CreateAt = DateTime.Now
            };
            var userId = await _userRepository.AddUserAsync(user);
            await _saveChangesRepository.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<User> LoginUser(LoginViewModel user)
        {
            user.Email = user.Email.ToLowerInvariant();
            user.Password = user.Password.EncodePasswordMd5();

            return await _userRepository.LoginUser(user.Email, user.Password);
        }

        public async Task<User> GetUserByUserId(int userId)
        {
            return await _userRepository.GetUserByUserId(userId);
        }

        public async Task<bool> IsUserInAdmin(int userId)
        {
            return await _userRoleRepository.IsUserInAdmin(userId);
        }

        public async Task<OpRes> DeleteUser(int userId)
        {
            var user = await _userRepository.GetUserByUserId(userId);
            if (user == null)
            {
                return OpRes.BuildError("User not found");
            }

            await _userRepository.DeleteUser(userId);
            await _saveChangesRepository.SaveChangesAsync();
            return OpRes.BuildSuccess();
        }

        private async Task<string> ValidateUserInfo(UserViewModel user)
        {
            if (user.Email.IsNullOrWhiteSpace())
            {
                return "Email can't be null";
            }
            if (user.Password.IsNullOrWhiteSpace())
            {
                return "Password can't be null";
            }

            var isEmailExist = await _userRepository.IsEmailExist(user.Email);
            if (isEmailExist)
            {
                return "Email is registered already";
            }
            return null;
        }

        public async Task<OpRes<PagedResult<UserDto, UserSearchItem>>> GetUserByPaging(UserSearchItem item)
        {
            var res = await _userRepository.GetUserByPaging(item);
            return OpRes<PagedResult<UserDto, UserSearchItem>>.BuildSuccess(res);
        }
    }
}
