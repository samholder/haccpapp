using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web.Mvc;
using hacapp.contracts.Commands;
using hacapp.core.Commands;
using Hacapp.Data;
using Hacapp.Data.Commands;
using Hacapp.Data.DataContexts;
using Hacapp.Data.DataContexts.ApplicationMigrations;
using Hacapp.Data.Query;
using hacapp.web.Controllers;
using Haccapp.Model.Identity;
using Microsoft.AspNet.Identity;
using Moq;
using SimpleInjector;
using TechTalk.SpecFlow;

namespace hacapp.web.Tests.Features.Teams
{
    public abstract class CommonSteps
    {
        private readonly CurrentScenarioContext context;
        private ApplicationDb db;
        private ICommandAndQueryDispatcher dispatcher;
        private UserManager<ApplicationUser> userManager;

        public CommonSteps(CurrentScenarioContext context)
        {
            this.context = context;
        }

        protected string CurrentLoggedInUserId { get; set; }

        [Before]
        public void ScenarioSetup()
        {
            if (context.SkipBefore)
            {
                return;
            }

            SetupContainer();
            CreateNewDb();
            context.SkipBefore = true;
        }

        [After]
        public void ScenarioDelete()
        {
            if (context.SkipAfter)
            {
                return;
            }

            db.Database.Delete();
            db.Dispose();
            context.SkipAfter = true;
        }

        private void CreateNewDb()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDb, Configuration>());

            if (db.Database.Exists())
            {
                db.Database.Delete();
            }

            db.Database.Initialize(true);
        }

        private void SetupContainer()
        {
            AutoMapperConfig.RegisterMappings();
            var container = new Container();

            container.RegisterSingle(() => new ApplicationDb());
            container.RegisterSingle<IUserStore<ApplicationUser>, ApplicationUserStore>();
            container.RegisterSingle<UserManager<ApplicationUser>>();

            //Register all of our Command and Query handlers in the application
            IEnumerable<Type> commandHandlerTypes =
                typeof (CreateNewTeamCommandHandler).Assembly.GetTypes()
                    .Where(x => typeof (ICommandHandler).IsAssignableFrom(x));
            IEnumerable<Type> queryHandlerTypes =
                typeof (GetAllTeamsQueryHandler).Assembly.GetTypes()
                    .Where(x => typeof (IQueryHandler).IsAssignableFrom(x));
            container.RegisterAll<ICommandHandler>(commandHandlerTypes);
            container.RegisterAll<IQueryHandler>(queryHandlerTypes);

            //Register the command and query dispatcher
            container.Register<ICommandAndQueryDispatcher, CommandAndQueryDispatcher>();

            container.Verify();

            dispatcher = container.GetInstance<ICommandAndQueryDispatcher>();
            db = container.GetInstance<ApplicationDb>();
            userManager = container.GetInstance<UserManager<ApplicationUser>>();
            context.TeamController = new TeamController(dispatcher);
            context.HomeController = new HomeController(dispatcher);
            context.Dispatcher = dispatcher;
            context.UserManager = userManager;
            context.Db = db;
        }

        protected Mock<ControllerContext> GetMockControllerContext(string principalUserId)
        {
            var mockContext = new Mock<ControllerContext>();
            IPrincipal genericPrincipal = TestHelper.GetPrincipal(principalUserId, principalUserId);
            mockContext.SetupGet(p => p.HttpContext.User).Returns(genericPrincipal);
            Thread.CurrentPrincipal = genericPrincipal;
            return mockContext;
        }
    }
}