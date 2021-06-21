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

            service.AddScoped<ICategoryService, CategoryService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<INewsService, NewsService>();
            service.AddScoped<ISliderService, SliderService>();

            #endregion

            #region Data Layer

            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<ISaveChangesRepository, SaveChangesRepository>();
            service.AddScoped<IUserRoleRepository, UserRoleRepository>();
            service.AddScoped<INewsRepository, NewsRepository>();
            service.AddScoped<INewsLikeRepository, NewsLikeRepository>();
            service.AddScoped<INewsCommentRepository, NewsCommentRepository>();
            service.AddScoped<ISliderRepository, SliderRepository>();

            #endregion
        }
    }
}
