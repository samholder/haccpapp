using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using Hacapp.Web.Models;
using hacapp.web.Tests.Features.Teams;
using TechTalk.SpecFlow;

namespace hacapp.web.Tests.Features.HomePage
{
    [Binding]
    public class HomePageSteps : CommonSteps
    {
        private readonly CurrentScenarioContext context;

        public HomePageSteps(CurrentScenarioContext context)
            : base(context)
        {
            this.context = context;
        }

        [Then(@"I should see that '(.*)' has a pending membership for team '(.*)'")]
        public void ThenIShouldSeeThatHasAPendingMembershipForTeam(string userId, int teamId)
        {
            var viewResult = context.ActionResult as ViewResult;
            viewResult.Should().NotBeNull();
            var model = viewResult.Model as HomePageViewModel;
            model.Should().NotBeNull();
            model.PendingMemberships.Should().HaveCount(1);
            model.PendingMemberships.First().TeamId.Should().Be(teamId);
            model.PendingMemberships.First().UserId.Should().Be(userId);
        }
    }
}