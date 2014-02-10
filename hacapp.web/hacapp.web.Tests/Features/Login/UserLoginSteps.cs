using System.Web.Mvc;
using FluentAssertions;
using hacapp.web.Tests.Features.Teams;
using TechTalk.SpecFlow;

namespace hacapp.web.Tests.Features.Login
{
    [Binding]
    public class UserLoginSteps : CommonSteps
    {
        private readonly CurrentScenarioContext context;


        public UserLoginSteps(CurrentScenarioContext context) : base(context)
        {
            this.context = context;
        }

        [When(@"I visit the home page")]
        public void WhenIVisitTheHomePage()
        {
            context.ActionResult = context.HomeController.Index();
        }

        [Then(@"home page should be displayed")]
        public void ThenHomePageShouldBeDisplayed()
        {
            context.ActionResult.Should().BeOfType<ViewResult>();
            var viewResult = context.ActionResult as ViewResult;
            string message = viewResult.ViewBag.Message;
            message.Should().Be("Welcome blah");
        }

        [Then(@"I Should be redirected to the team index page")]
        public void ThenIShouldBeRedirectedToTheTeamIndexPage()
        {
            var redirectResult = context.ActionResult as RedirectToRouteResult;
            redirectResult.RouteValues["action"].Should().Be(@"Index");
        }

        [Then(@"I Should see the home page")]
        public void ThenIShouldSeeTheHomePage()
        {
            var viewResult = context.ActionResult as ViewResult;
            viewResult.Should().NotBeNull();
        }
    }
}