using System.Linq;
using System.Threading;
using Haccapp.Model.Identity;
using Microsoft.AspNet.Identity;

namespace Hacapp.Data.Identity
{
    public static class ApplicationUserExtensions
    {
        public static bool IsCurrentUser(this ApplicationUser user)
        {
            return user.Id == Thread.CurrentPrincipal.Identity.GetUserId();
        }

        public static bool IsInRole(this ApplicationUser user, RoleName roleName)
        {
            return user.Roles.Any(x => x.Role.Name == RoleName.TeamManagement);
        }
    }
}