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

namespace MyCms.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISaveChangesRepository _saveChangesRepository;

        public UserService(IUserRepository userRepository, ISaveChangesRepository saveChangesRepository)
        {
            _userRepository = userRepository;
            _saveChangesRepository = saveChangesRepository;
        }

        public async Task<OpRes> AddUserAsync(UserViewModel userViewModel)
        {
            var validate = ValidateUserInfo(userViewModel);
            if (validate.IsNullOrWhiteSpace() == false)
            {
                return OpRes.BuildError(validate);
            }

            await AddRawUser(userViewModel);

            //TODO Add User In User Role Table

            return OpRes.BuildSuccess();

        }

        private async Task AddRawUser(UserViewModel userViewModel)
        {
            var user = new User()
            {
                Email = userViewModel.Email.ToLowerInvariant(),
                Password = userViewModel.Password.EncodePasswordMd5()
            };
            await _userRepository.AddUserAsync(user);
        }

        public async Task<OpRes> AddAdminAsync(UserViewModel userViewModel)
        {
            var validate = ValidateUserInfo(userViewModel);
            if (validate.IsNullOrWhiteSpace() == false)
            {
                return OpRes.BuildError(validate);
            }

            await AddRawUser(userViewModel);

            //TODO Add User In User Role Table

            return OpRes.BuildSuccess();
        }

        public async Task<User> LoginUser(UserViewModel user)
        {
            return await _userRepository.LoginUser(user.Email, user.Password);
        }

        public async Task<User> GetUserByUserId(int userId)
        {
            return await _userRepository.GetUserByUserId(userId);
        }

        public Task<bool> IsUserInAdmin(int userId)
        {
            throw new NotImplementedException();
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

        private static string ValidateUserInfo(UserViewModel user)
        {
            if (user.Email.IsNullOrWhiteSpace())
            {
                return "Email can't be null";
            }
            if (user.Password.IsNullOrWhiteSpace())
            {
                return "Password can't be null";
            }

            return null;
        }

    }
}
