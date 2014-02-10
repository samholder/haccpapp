using System.Threading;
using hacapp.contracts.Commands;
using Hacapp.Core.Commands;
using Hacapp.Core.Queries;
using Hacapp.Data.DataContexts;
using Hacapp.Data.Identity;
using Haccapp.Model.Domain;
using Haccapp.Model.Identity;
using Microsoft.AspNet.Identity;

namespace Hacapp.Data.Commands
{
    internal class DeleteTeamCommandHandler : CommandHandlerBase<DeleteTeamCommand>
    {
        private readonly ApplicationDb db;

        public DeleteTeamCommandHandler(ApplicationDb db)
        {
            this.db = db;
        }

        public override void Execute(DeleteTeamCommand command, ICommandAndQueryDispatcher dispatcher)
        {
            Team team = dispatcher.ExecuteQuery(new GetTeamByIdQuery(command.TeamId));
            if (CurrentUserCanDeleteTeams(team, dispatcher))
            {
                db.Teams.Remove(team);
                db.SaveChanges();
            }
        }

        private static bool CurrentUserCanDeleteTeams(Team team, ICommandAndQueryDispatcher dispatcher)
        {
            if (!team.Owner.IsCurrentUser())
            {
                ApplicationUser user =
                    dispatcher.ExecuteQuery(new GetUserByIdQuery(Thread.CurrentPrincipal.Identity.GetUserId()));
                if (!user.IsInRole(RoleName.TeamManagement))
                {
                    return false;
                }
            }

            return true;
        }
    }
}