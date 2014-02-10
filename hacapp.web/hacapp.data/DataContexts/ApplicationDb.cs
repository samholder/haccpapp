using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Haccapp.Model.Domain;
using Haccapp.Model.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Hacapp.Data.DataContexts
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDb : IdentityDbContext<ApplicationUser>
    {
        private static readonly Guid dbName = Guid.NewGuid();

        private static readonly string NameOrConnectionString =
            @"Server=(localdb)\v11.0;Integrated Security=true;Initial Catalog=" + dbName +
            ";AttachDbFileName= e:\\localdb\\" + dbName + ".mdf";

#if NCRUNCH
        public ApplicationDb()
            : base(InitialCatalog.ToString())
        {       
            Configuration.LazyLoadingEnabled = false;
            Database.Log = s => Log(s);
        }
#else
        public ApplicationDb()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Database.Log = s => Log(s);
        }
#endif

        private void Log(string s)
        {
            Trace.WriteLine(s);
        }

        /// <summary>
        ///     Property for updating the Teams
        /// </summary>
        public IDbSet<Team> Teams { get; set; }

        public static string InitialCatalog
        {
            get { return NameOrConnectionString; }
        }
    }
}