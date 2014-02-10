using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using hacapp.contracts.Commands;
using Hacapp.Core.Queries;
using Hacapp.Core.Queries.Result;
using Hacapp.Web.Models;
using Haccapp.Model.Domain;
using Microsoft.AspNet.Identity;

namespace hacapp.web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICommandAndQueryDispatcher dispatcher;

        public HomeController(ICommandAndQueryDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public ActionResult Index()
        {
            string userId = HttpContext.User.Identity.GetUserId();
            IEnumerable<Team> teams =
                dispatcher.ExecuteQuery(new GetUserTeamsQuery(userId));
            if (!teams.Any())
            {
                return RedirectToAction("Index", "Team");
            }

            ViewBag.Message = "Welcome " + ControllerContext.HttpContext.User.Identity.Name;
            IEnumerable<PendingMembershipResult> pendingMemberships =
                dispatcher.ExecuteQuery(new GetPendingMembershipsForUsersTeamsQuery(userId));
            var model = new HomePageViewModel
            {
                PendingMemberships =
                    Mapper.Map<IEnumerable<PendingMembershipResult>, List<PendingMembershipModel>>(pendingMemberships)
            };

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}