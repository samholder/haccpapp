using hacapp.contracts.Commands;
using Hacapp.Core.Commands;
using Hacapp.Core.Queries;
using Hacapp.Data.DataContexts;
using Haccapp.Model.Domain;

namespace Hacapp.Data.Commands
{
    internal class JoinATeamCommandHandler : CommandHandlerBase<JoinTeamCommand>
    {
        private readonly ApplicationDb db;

        public JoinATeamCommandHandler(ApplicationDb db)
        {
            this.db = db;
        }

        public override void Execute(JoinTeamCommand command, ICommandAndQueryDispatcher dispatcher)
        {
            var teamQuery = new GetTeamByIdQuery(command.TeamId);
            Team team = dispatcher.ExecuteQuery(teamQuery);
            if (UserIsNotAMemberOfTeam(command.GetUserId, team))
            {
                var userQuery = new GetUserByIdQuery(command.GetUserId);
                team.Members.Add(new TeamMembership(dispatcher.ExecuteQuery(userQuery), MembershipStatus.Pending));
            }

            db.SaveChanges();
        }

        private static bool UserIsNotAMemberOfTeam(string userId, Team team)
        {
            return !(team.Owner.Id == userId || team.Members.Exists(t => t.User.Id == userId));
        }
    }
}