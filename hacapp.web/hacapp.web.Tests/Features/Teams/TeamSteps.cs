using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using Hacapp.Core.Commands;
using Hacapp.Core.Queries;
using Hacapp.Web.Models.Team;
using Haccapp.Model.Domain;
using Haccapp.Model.Identity;
using Microsoft.AspNet.Identity;
using Moq;
using TechTalk.SpecFlow;

namespace hacapp.web.Tests.Features.Teams
{
    [Binding]
    public class TeamSteps : CommonSteps
    {
        private readonly CurrentScenarioContext context;

        public TeamSteps(CurrentScenarioContext context) : base(context)
        {
            this.context = context;
        }

        [Given(@"There is a registered user with id '(.*)'")]
        public void GivenThereIsARegisteredUserWithId(string userId)
        {
            var user = new ApplicationUser
            {
                UserName = userId,
                Id = userId
            };
            IdentityResult result = context.UserManager.Create(user);
            result.Succeeded.Should().BeTrue();
            var info = new UserLoginInfo("test", "test");
            context.UserManager.AddLogin(user.Id, info);
        }

        [Given(@"Team '(.*)' is owned by '(.*)'")]
        public void GivenTeamIsOwnedBy(string teamId, string userId)
        {
            ControllerContext currentContext = context.TeamController.ControllerContext;
            Mock<ControllerContext> mockContext = GetMockControllerContext(userId);
            context.TeamController.ControllerContext = mockContext.Object;
            context.TeamController.Create(new CreateTeamViewModel {TeamName = teamId});
            context.TeamController.ControllerContext = currentContext;
        }

        [When(@"I visit the team details page for team '(.*)'")]
        public void WhenIVisitTheTeamDetailsPageForTeam(int teamId)
        {
            context.ActionResult = context.TeamController.Details(teamId);
        }

        [Then(@"'(.*)' should be listed in the pending members")]
        public void ThenShouldBeListedInThePendingMembers(string userId)
        {
            var viewResult = context.ActionResult as ViewResult;
            var teamDetailsModel = viewResult.Model as TeamDetailsViewModel;
            teamDetailsModel.PendingMembers.Should().Contain(m => m.Id == userId);
        }

        [Then(@"'(.*)' should be listed in the confirmed members")]
        public void ThenShouldBeListedInTheConfirmedMembers(string userId)
        {
            var viewResult = context.ActionResult as ViewResult;
            var teamDetailsModel = viewResult.Model as TeamDetailsViewModel;
            teamDetailsModel.ConfirmedMembers.Should().Contain(m => m.Id == userId);
        }

        [Given(@"I am logged in as '(.*)'")]
        public void GivenIAmLoggedInAs(string userId)
        {
            Mock<ControllerContext> mockContext = GetMockControllerContext(userId);
            context.SetControllerContexts(mockContext);
            CurrentLoggedInUserId = userId;
        }

        [Then(@"The list of teams to join should contain Team '(.*)'")]
        public void ThenTheListOfTeamsToJoinShouldContainTeam(string teamName)
        {
            var viewResult = context.ActionResult as ViewResult;
            viewResult.Model.As<IEnumerable<TeamDetailsViewModel>>().Should().Contain(t => t.Name == teamName);
        }

        [Then(@"The list of teams to join should not contain Team '(.*)'")]
        public void ThenTheListOfTeamsToJoinShouldNotContainTeam(string teamName)
        {
            var viewResult = context.ActionResult as ViewResult;
            viewResult.Model.As<IEnumerable<TeamDetailsViewModel>>().Should().NotContain(t => t.Name == teamName);
        }


        [Then(@"a team called '(.*)' should be created and '(.*)' should be owner of that team")]
        public void ThenANewTeamShouldBeCreatedAndTheUserShouldBeOwnerOfThatTeam(string teamName, string userId)
        {
            List<Team> createdTeams = context.Db.Teams.Where(t => t.Name == teamName).ToList();
            createdTeams.Should().HaveCount(1);
            createdTeams.First().Owner.Id.Should().Be(userId);
        }


        [When(@"I enter the full details for creating a team called '(.*)'")]
        public void WhenIEnterTheFullDetailsForCreatingATeamCalled(string teamName)
        {
            context.ActionResult =
                context.TeamController.Create(new CreateTeamViewModel {TeamName = teamName});
        }

        [Then(@"I should be redirected to the team details page")]
        public void ThenIShouldBeRedirectedToTheTeamDetailsPage()
        {
            context.ActionResult.Should().BeOfType<RedirectToRouteResult>();
            var redirectResult = context.ActionResult as RedirectToRouteResult;
            redirectResult.RouteValues.Should().Contain("id", 1)
                .And.Contain("action", "Details");
        }

        [Then(@"only one team called '(.*)' should not be created")]
        public void ThenOnlyOneTeamCalledShouldNotBeCreated(string teamName)
        {
            List<Team> createdTeams = context.Db.Teams.Where(t => t.Name == teamName).ToList();
            createdTeams.Should().HaveCount(1);
        }

        [Then(@"I should be redirected to back to the create a team page")]
        public void ThenIShouldBeRedirectedToBackToTheCreateATeamPage()
        {
            context.ActionResult.Should().BeOfType<ViewResult>();
            var viewResult = context.ActionResult as ViewResult;
        }

        [Then(@"there should be an error message")]
        public void ThenThereShouldBeAnErrorMessage()
        {
            context.ActionResult.Should().BeOfType<ViewResult>();
            var viewResult = context.ActionResult as ViewResult;
            ((string) viewResult.ViewBag.ErrorMessage).Should()
                .Be(string.Format("The team name was invalid.  A team called 'BobsTeam' already exists."));
        }

        [When(@"I vist the join a team page")]
        public void WhenIVistTheJoinATeamPage()
        {
            context.ActionResult = context.TeamController.Join(null);
        }

        [Then(@"I should be shown the No Teams Exist view")]
        public void ThenIShouldBeShownTheNoTeamsExistView()
        {
            var viewResult = context.ActionResult as ViewResult;
            viewResult.ViewName.Should().Be("NoTeamToJoin");
        }


        [When(@"I try and join team '(.*)'")]
        public void WhenITryAndJoinTeam(int teamId)
        {
            context.ActionResult = context.TeamController.Join(teamId);
        }

        [Then(@"'(.*)' should have a pending membership for team '(.*)'")]
        public void ThenIShouldHaveAPendingMembershipForTeam(string userId, int teamId)
        {
            var teamQuery = new GetTeamByIdQuery(teamId);
            Team team = context.Dispatcher.ExecuteQuery(teamQuery);
            team.Members.Any(x => x.User.Id == userId && x.Status == MembershipStatus.Pending).Should().BeTrue();
        }

        [Then(@"'(.*)' should not have a pending membership for team '(.*)'")]
        public void ThenShouldNotHaveAPendingMembershipForTeam(string userId, int teamId)
        {
            var teamQuery = new GetTeamByIdQuery(teamId);
            Team team = context.Dispatcher.ExecuteQuery(teamQuery);
            team.Members.Any(x => x.User.Id == userId && x.Status == MembershipStatus.Pending).Should().BeFalse();
        }


        [Then(@"Team '(.*)' should be listed on my teams page")]
        public void ThenTeamShouldBeListedOnMyTeamsPage(int teamId)
        {
            context.ActionResult = context.TeamController.Index();
            var viewResult = context.ActionResult.As<ViewResult>();
            var teams = viewResult.Model.As<IEnumerable<TeamDetailsViewModel>>();
            teams.Should()
                .Contain(
                    t =>
                        t.Id == teamId);
        }

        [Then(@"I should be redirected to my teams page")]
        public void ThenIShouldBeRedirectedMyTeamsPage()
        {
            var redirect = context.ActionResult as RedirectToRouteResult;
            redirect.RouteValues["action"].Should().Be("Index");
        }

        [Then(@"I should be redirected to team details page for team '(.*)'")]
        public void ThenIShouldBeRedirectedToTeamDetailsPageForTeam(int teamId)
        {
            var redirect = context.ActionResult as RedirectToRouteResult;
            redirect.RouteValues["id"].Should().Be(teamId);
            redirect.RouteValues["action"].Should().Be("Details");
        }


        [Then(@"I should see a list of teams that I can join")]
        public void ThenIShouldSeeAListOfTeamsThatICanJoin()
        {
            context.ActionResult.Should().BeOfType<ViewResult>();
            var teams = context.ActionResult.As<ViewResult>()
                .Model.As<IEnumerable<Team>>();
            teams
                .Should()
                .Contain(t => t.Id == 1 && t.Name == "team1")
                .And.Contain(t => t.Id == 2 && t.Name == "team2")
                .And.HaveCount(2);
        }

        [Given(@"'(.*)' has pending membership for Team '(.*)'")]
        public void GivenHasPendingMembershipForTeam(string userId, int teamId)
        {
            var command = new JoinTeamCommand(teamId, userId);
            context.Dispatcher.ExecuteCommand(command);
        }

        [When(@"I accept pending membership for '(.*)' to join team '(.*)'")]
        public void WhenIAcceptPendingMembershipFor(string userId, int teamId)
        {
            context.ActionResult = context.TeamController.UpdateMembership(teamId, userId,
                UserMembershipStatus.Confirmed.ToString());
        }

        [Then(@"'(.*)' should be a full member of team '(.*)'")]
        public void ThenUserShouldBeFullMemberOfTeamBlah(string userId, int teamId)
        {
            var userTeamsQuery = new GetUserTeamsQuery(userId);
            IEnumerable<Team> teams = context.Dispatcher.ExecuteQuery(userTeamsQuery);
            teams.Should()
                .Contain(
                    t =>
                        t.Members.SingleOrDefault(m => m.User.Id == userId && m.Status == MembershipStatus.Confimed) !=
                        null);
        }

        [When(@"I visit the details page for team '(.*)'")]
        public void WhenIVisitTheDetailsPageForTeam(int teamId)
        {
            context.ActionResult = context.TeamController.Details(teamId);
        }

        [Then(@"I should see the details for the team '(.*)'")]
        public void ThenIShouldSeeTheDetailsForTheTeam(int teamId)
        {
            var viewResult = context.ActionResult as ViewResult;
            var model = viewResult.Model as TeamDetailsViewModel;
            model.Id.Should().Be(teamId);
        }

        [Then(@"I should see a not authorized")]
        public void ThenIShouldSeeANotAuthorized()
        {
            var viewResult = context.ActionResult as ViewResult;
            viewResult.Model.Should().BeNull();
            var errorMessage = viewResult.ViewBag.ErrorMessage as string;
            errorMessage.Should().Be("You are not authorized to view this teams details");
        }

        [When(@"I go to the home page")]
        public void WhenIGoToTheHomePage()
        {
            context.ActionResult = context.TeamController.Index();
        }

        [Then(@"I should the create or join view")]
        public void ThenIShouldSeeTheCreateOrJoinView()
        {
            var viewResult = context.ActionResult as ViewResult;
            viewResult.Model.Should().BeNull();
            viewResult.ViewName.Should().Be("IndexNoTeams");
        }

        [Given(@"'(.*)' is a member Team '(.*)'")]
        public void GivenIsAMemberTeam(string userId, int teamId)
        {
            context.Dispatcher.ExecuteCommand(new JoinTeamCommand(teamId, userId));
            context.Dispatcher.ExecuteCommand(new UpdateMembershipStatusCommand(userId, teamId,
                MembershipStatus.Confimed));
        }

        [When(@"I remove '(.*)' from team '(.*)'")]
        public void WhenIRemoveFromTeam(string userId, int teamId)
        {
            context.ActionResult =
                context.TeamController.UpdateMembership(teamId, userId, UserMembershipStatus.Removed.ToString());
        }

        [Then(@"'(.*)' should not be a member of team '(.*)' any more")]
        public void ThenShouldNotBeAMemberOfTeamAnyMore(string userId, int teamId)
        {
            Team team = context.Dispatcher.ExecuteQuery(new GetTeamByIdQuery(teamId));
            team.Members.Should().NotContain(m => m.User.Id == userId);
        }

        [When(@"I delete team '(.*)'")]
        public void WhenIDeleteTeam(int teamId)
        {
            context.TeamController.Delete(teamId);
        }

        [Then(@"team '(.*)' should not exist")]
        public void ThenTeamShouldNotExist(int teamId)
        {
            Action action = () => context.Dispatcher.ExecuteQuery(new GetTeamByIdQuery(teamId));
            action.ShouldThrow<CommandExecutionException>()
                .And.Message.Should()
                .Be(CommandExecutionExceptionMessages.TeamDoesNotExist(teamId));
        }

        [Then(@"'(.*)' should not be a member of any teams")]
        public void ThenShouldNotBeAMemberOfAnyTeams(string userId)
        {
            IEnumerable<Team> teams = context.Dispatcher.ExecuteQuery(new GetUserTeamsQuery(userId));
            teams.Should().HaveCount(0);
        }

        [Given(@"'(.*)' is in the TeamManagement role")]
        public void GivenIsAnAdministrator(string userId)
        {
            context.UserManager.AddToRoleAsync(userId, RoleName.TeamManagement).Result.Succeeded.Should().BeTrue();
        }
    }
}