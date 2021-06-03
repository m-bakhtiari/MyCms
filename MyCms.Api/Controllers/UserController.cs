using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MyCms.Core.Interfaces;
using MyCms.Core.Services;
using MyCms.Core.ViewModels;
using MyCms.Domain.Dto;
using MyCms.Extensions.Extensions;

namespace MyCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [PermissionChecker]
    public class UserController : ControllerBase
    {
        #region Constructor

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetUser(UserSearchItem item)
        {
            var res = await _userService.GetUserByPaging(item);
            return Ok(res);
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterUser(UserViewModel user)
        {
            var res = await _userService.RegisterUser(user);
            if (res.IsSuccess == false)
            {
                return res.ToBadRequestError();
            }
            return Ok();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var role = await _userService.DeleteUser(id);
            if (role.IsSuccess == false)
            {
                return role.ToBadRequestError();
            }

            return Ok();
        }
    }
}
