using MyCms.Core.Extensions;
using MyCms.Core.Interfaces;
using MyCms.Core.Mapper;
using MyCms.Core.ViewModels;
using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;
using System.Threading.Tasks;

namespace MyCms.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<Role> GetRoleByRoleIdAsync(int roleId)
        {
            return await _roleRepository.GetRoleByRoleIdAsync(roleId);
        }

        public async Task<OpRes> AddAsync(RolesViewModel rolesViewModel)
        {
            var error = RoleValidate(rolesViewModel);
            if (error.IsNullOrWhiteSpace() == false)
            {
                return OpRes.BuildError(error);
            }

            var role = MapViewModelToEntity.ToRole(rolesViewModel);

            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveChangesAsync();
            return OpRes.BuildSuccess();
        }

        public async Task<OpRes> UpdateAsync(RolesViewModel rolesViewModel)
        {
            var error = RoleValidate(rolesViewModel);
            if (error.IsNullOrWhiteSpace() == false)
            {
                return OpRes.BuildError(error);
            }

            var role = MapViewModelToEntity.ToRole(rolesViewModel);

            await _roleRepository.UpdateAsync(role);
            await _roleRepository.SaveChangesAsync();
            return OpRes.BuildSuccess();
        }

        public async Task<OpRes> DeleteRoleAsync(int roleId)
        {
            var role = await _roleRepository.GetRoleByRoleIdAsync(roleId);
            if (role == null)
            {
                return OpRes.BuildError("Not Found");
            }
            await _roleRepository.DeleteRoleAsync(roleId);
            await _roleRepository.SaveChangesAsync();
            return OpRes.BuildSuccess();
        }

        private static string RoleValidate(RolesViewModel rolesViewModel)
        {
            if (rolesViewModel.Title.IsNullOrWhiteSpace())
            {
                return "Title can't be null";
            }

            return null;
        }
    }
}
