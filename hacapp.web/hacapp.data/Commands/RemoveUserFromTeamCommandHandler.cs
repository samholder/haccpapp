using System.Linq;
using System.Threading;
using hacapp.contracts.Commands;
using Hacapp.Core.Commands;
using Hacapp.Core.Queries;
using Hacapp.Data.DataContexts;
using Hacapp.Data.Identity;
using Haccapp.Model.Domain;
using Microsoft.AspNet.Identity;

namespace Hacapp.Data.Commands
{
    internal class RemoveUserFromTeamCommandHandler : CommandHandlerBase<RemoveUserFromTeamCommand>
    {
        private readonly ApplicationDb db;

        public RemoveUserFromTeamCommandHandler(ApplicationDb db)
        {
            this.db = db;
        }

        public override void Execute(RemoveUserFromTeamCommand command, ICommandAndQueryDispatcher dispatcher)
        {
            Team team = dispatcher.ExecuteQuery(new GetTeamByIdQuery(command.TeamId));
            if (CurrentUserIsAllowedToRemoveTargetUser(command, team))
            {
                TeamMembership membership = team.Members.SingleOrDefault(m => m.User.Id == command.UserId);
                if (membership != null)
                {
                    team.Members.Remove(membership);
                }

                db.SaveChanges();
            }
        }

        private static bool CurrentUserIsAllowedToRemoveTargetUser(RemoveUserFromTeamCommand command, Team team)
        {
            return team.Owner.IsCurrentUser() || Thread.CurrentPrincipal.Identity.GetUserId() == command.UserId;
        }
    }
}