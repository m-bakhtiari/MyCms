using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Core.Extensions
{
    public static class IdentityUserExtensions
    {
        [DebuggerStepThrough]
        public static Guid? TryGetUserId(this IPrincipal principal)
        {
            try
            {
                var simplePrinciple = (ClaimsPrincipal)principal;
                var identity = simplePrinciple.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                    return Guid.Parse(userIdClaim.Value);
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        [DebuggerStepThrough]
        public static string TryGetUsername(this IPrincipal principal)
        {
            return principal?.Identity?.Name;
        }

        [DebuggerStepThrough]
        public static int GetUserId(this IPrincipal principal)
        {
            try
            {
                var simplePrinciple = (ClaimsPrincipal)principal;
                var identity = simplePrinciple.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                    return int.Parse(userIdClaim.Value);
                }
                throw new Exception("User id is not defined");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
