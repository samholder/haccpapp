using System;
using System.Data.Entity.Migrations;
using Haccapp.Model.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Hacapp.Data.DataContexts.ApplicationMigrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\ApplicationMigrations";
        }

        protected override void Seed(ApplicationDb context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists(RoleName.TeamManagement))
            {
                roleManager.Create(new IdentityRole(RoleName.TeamManagement));
            }

            const string siteOwnerId = "SiteOwner";
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser sam = userManager.FindById(siteOwnerId);
            if (sam == null)
            {
                sam = new ApplicationUser
                {
                    Id = siteOwnerId,
                    PreferredEmailAddress = "samholder@gmail.com",
                    UserName = "SamHolder"
                };
                IdentityResult identityResult = userManager.Create(sam);
                if (identityResult.Succeeded)
                {
                    IdentityResult result = userManager.AddLoginAsync(siteOwnerId,
                        new UserLoginInfo("Google",
                            "https://www.google.com/accounts/o8/id?id=AItOawllqzkU2MreNrjy29lR08H0AVzjd-Y4T6s")).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception("Problem setting up the db");
                    }

                    if (!userManager.AddToRoleAsync(siteOwnerId, RoleName.TeamManagement).Result.Succeeded)
                    {
                        throw new Exception("Unable to add sam to the role of site admin");
                    }
                }
                else
                {
                    throw new Exception("Problem creating the default user " + string.Join(",", identityResult.Errors));
                }
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}