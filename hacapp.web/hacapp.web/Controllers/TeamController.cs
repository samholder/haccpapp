using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using hacapp.contracts.Commands;
using Hacapp.Core.Commands;
using Hacapp.Core.Queries;
using Hacapp.Data.Identity;
using Hacapp.Web.Models.Team;
using Haccapp.Model.Domain;
using Haccapp.Model.Identity;
using Microsoft.AspNet.Identity;

namespace hacapp.web.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        private readonly ICommandAndQueryDispatcher dispatcher;

        public TeamController(ICommandAndQueryDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        //
        // GET: /Team/
        public ActionResult Index()
        {
            IQuery<IEnumerable<Team>> command = new GetUserTeamsQuery(HttpContext.User.Identity.GetUserId());
            IEnumerable<Team> teams = dispatcher.ExecuteQuery(command).ToList();
            if (teams.Any())
            {
                List<TeamDetailsViewModel> teamDetailsViewModels =
                    Mapper.Map<IEnumerable<Team>, List<TeamDetailsViewModel>>(teams);
                ApplicationUser currentUser =
                    dispatcher.ExecuteQuery(new GetUserByIdQuery(HttpContext.User.Identity.GetUserId()));
                foreach (TeamDetailsViewModel team in teamDetailsViewModels)
                {
                    team.IsEditable = CurrentUserIsOwnerOrTeamManager(currentUser, team);
                }
                return View(teamDetailsViewModels);
            }

            ViewBag.Message = "Welcome " + ControllerContext.HttpContext.User.Identity.Name;
            return View("IndexNoTeams");
        }

        private bool CurrentUserIsOwnerOrTeamManager(ApplicationUser currentUser, TeamDetailsViewModel team)
        {
            return currentUser.IsInRole(RoleName.TeamManagement) || team.Owner.Id == currentUser.Id;
        }

        //
        // GET: /Team/Details/5
        public ActionResult Details(int id)
        {
            var query = new GetUserConfirmedTeamsQuery(HttpContext.User.Identity.GetUserId());
            Team team = dispatcher.ExecuteQuery(query).FirstOrDefault(t => t.Id == id);
            if (team != null)
            {
                TeamDetailsViewModel teamViewModel = Mapper.Map<Team, TeamDetailsViewModel>(team);
                ApplicationUser currentUser =
                    dispatcher.ExecuteQuery(new GetUserByIdQuery(HttpContext.User.Identity.GetUserId()));
                teamViewModel.IsEditable = CurrentUserIsOwnerOrTeamManager(currentUser, teamViewModel);
                return View(teamViewModel);
            }
            ViewBag.ErrorMessage = "You are not authorized to view this teams details";
            return View("NotAMemberOfATeam");
        }

        //
        // GET: /Team/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Team/Create
        [HttpPost]
        public ActionResult Create(CreateTeamViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IQuery<ApplicationUser> currentUser = new GetUserByIdQuery(HttpContext.User.Identity.GetUserId());
                    var command = new CreateNewTeamCommand(viewModel.TeamName, dispatcher.ExecuteQuery(currentUser));
                    dispatcher.ExecuteCommand(command);

                    return RedirectToAction("Details", new {id = command.TeamId});
                }
                catch (ArgumentException argumentException)
                {
                    ViewBag.ErrorMessage = argumentException.Message;
                    return View();
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: Team/Join/5
        public ActionResult Join(int? teamId)
        {
            if (teamId == null)
            {
                return ShowTeamsToJoin();
            }

            return JoinExistingTeam(teamId);
        }

        private ActionResult JoinExistingTeam(int? teamId)
        {
            var command = new JoinTeamCommand(teamId.Value, HttpContext.User.Identity.GetUserId());
            dispatcher.ExecuteCommand(command);
            return RedirectToAction("Index");
        }

        private ActionResult ShowTeamsToJoin()
        {
            var query = new GetTeamsUserDoesNotBelongToQuery(HttpContext.User.Identity.GetUserId());
            IEnumerable<Team> teams = dispatcher.ExecuteQuery(query).ToList();
            if (teams.Any())
            {
                return View(Mapper.Map<IEnumerable<Team>, List<TeamDetailsViewModel>>(teams));
            }

            return View("NoTeamToJoin");
        }

        //GET: Team/5/UpdateMembership/1/NewStatus
        public ActionResult UpdateMembership(int teamId, string idOfUserToUpdate, string newStatus)
        {
            if (ModelState.IsValid)
            {
                UserMembershipStatus newMembershipStatus;
                if (!Enum.TryParse(newStatus, true, out newMembershipStatus))
                {
                    return RedirectToAction("Details", new {id = teamId});
                }

                ICommand command = CreateCommandToUpdateMembership(teamId, idOfUserToUpdate, newMembershipStatus);

                try
                {
                    dispatcher.ExecuteCommand(command);
                }
                catch (InvalidOperationException exception)
                {
                    ViewBag.ErrorMessage = exception.Message;
                }

                return RedirectToAction("Details", new {id = teamId});
            }

            return RedirectToAction("Details", new {id = teamId});
        }

        private static ICommand CreateCommandToUpdateMembership(int teamId, string idOfUserToUpdate,
            UserMembershipStatus newMembershipStatus)
        {
            ICommand command = null;
            if (newMembershipStatus == UserMembershipStatus.Removed)
            {
                command = new RemoveUserFromTeamCommand(idOfUserToUpdate, teamId);
            }
            else
            {
                command = new UpdateMembershipStatusCommand(idOfUserToUpdate, teamId,
                    Mapper.Map<UserMembershipStatus, MembershipStatus>(
                        newMembershipStatus));
            }
            return command;
        }


        //POST: Team/5/Delete
        [HttpPost]
        public void Delete(int teamId)
        {
            dispatcher.ExecuteCommand(new DeleteTeamCommand(teamId));
        }
    }
}