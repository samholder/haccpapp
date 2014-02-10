using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using hacapp.contracts.Commands;
using hacapp.core.Commands;
using Hacapp.Data;
using Hacapp.Data.Commands;
using Hacapp.Data.DataContexts;
using Hacapp.Data.Query;
using Haccapp.Model;
using Haccapp.Model.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using SimpleInjector.Extensions;
using SimpleInjector.Integration.Web.Mvc;

namespace hacapp.web
{
    public static class SimpleInjectorConfig
    {
        public static void RegisterComponents()
        {
            // Create the container as usual.
            var container = new Container();

            container.RegisterPerWebRequest<ApplicationDb>();
            container.RegisterPerWebRequest<IUserStore<ApplicationUser>, ApplicationUserStore>();
            container.RegisterPerWebRequest<UserManager<ApplicationUser>>();
            //register the user manager
//            container.RegisterPerWebRequest(
//                () => new UserManager<ApplicationUser>(new ApplicationUserStore(applicationDb)));

            container.Register(() => GlobalConfiguration.Configuration);
            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcAttributeFilterProvider();

            //Register all of our Command and Query handlers in the application
            IEnumerable<Type> commandHandlerTypes = typeof (CreateNewTeamCommandHandler).Assembly.GetTypes().Where(x=>typeof(ICommandHandler).IsAssignableFrom(x));
            IEnumerable<Type> queryHandlerTypes = typeof(GetAllTeamsQueryHandler).Assembly.GetTypes().Where(x => typeof(IQueryHandler).IsAssignableFrom(x));
            container.RegisterAll<ICommandHandler>(commandHandlerTypes);
            container.RegisterAll<IQueryHandler>(queryHandlerTypes);

            //Register the command and query dispatcher
            container.Register<ICommandAndQueryDispatcher, CommandAndQueryDispatcher>();

            container.Verify();

            DependencyResolver.SetResolver(
                new SimpleInjectorDependencyResolver(container));
        }
    }
}