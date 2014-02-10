using System.Web.Mvc;
using hacapp.contracts.Commands;
using Hacapp.Data.DataContexts;
using hacapp.web.Controllers;
using Haccapp.Model.Identity;
using Microsoft.AspNet.Identity;
using Moq;

namespace hacapp.web.Tests.Features
{
    /// <summary>
    ///     Class which represents the context in which the current scenario is executing.  This is used to share state between
    ///     the various step definitions.
    /// </summary>
    public class CurrentScenarioContext
    {
        public ActionResult ActionResult { get; set; }
        public TeamController TeamController { get; set; }
        public HomeController HomeController { get; set; }
        public ICommandAndQueryDispatcher Dispatcher { get; set; }
        public UserManager<ApplicationUser> UserManager { get; set; }
        public ApplicationDb Db { get; set; }
        public bool SkipBefore { get; set; }
        public bool SkipAfter { get; set; }

        public void SetControllerContexts(Mock<ControllerContext> mockContext)
        {
            TeamController.ControllerContext = mockContext.Object;
            HomeController.ControllerContext = mockContext.Object;
        }
    }
}