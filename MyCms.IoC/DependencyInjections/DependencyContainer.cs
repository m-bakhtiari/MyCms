using Microsoft.Extensions.DependencyInjection;
using MyCms.Core.Interfaces;
using MyCms.Core.Services;
using MyCms.Data.Repositories;
using MyCms.Domain.Interfaces;

namespace MyCms.IoC.DependencyInjections
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {
            #region Core Layer

            service.AddScoped<IRoleService, RoleService>();

            #endregion

            #region Data Layer

            service.AddScoped<IRoleRepository, RoleRepository>();

            #endregion
        }
    }
}
