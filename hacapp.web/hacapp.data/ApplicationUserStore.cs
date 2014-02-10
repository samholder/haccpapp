using System.Data.Entity;
using Hacapp.Data.DataContexts;
using Haccapp.Model.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Hacapp.Data
{
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        private ApplicationUserStore()
        {
        }

        public ApplicationUserStore(ApplicationDb context) : base(context)
        {
        }
    }
}