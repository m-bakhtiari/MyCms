
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyCms.Core.Extensions;
using MyCms.Core.Interfaces;

namespace MyCms.Core.Services
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IUserService _userService;
        public PermissionCheckerAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _userService = (UserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = context.HttpContext.User.GetUserId();
                var isAdmin = _userService.IsUserInAdmin(userId);
                if (isAdmin.Result == false)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
