using System;
using System.Linq;
using hacapp.contracts.Commands;
using Hacapp.Core.Commands;
using Hacapp.Core.Queries;
using Hacapp.Data.DataContexts;
using Hacapp.Data.Identity;
using Haccapp.Model.Domain;

namespace Hacapp.Data.Commands
{
    internal class UpdateMembershipStatusCommandHandler : CommandHandlerBase<UpdateMembershipStatusCommand>
    {
        private readonly ApplicationDb db;

        public UpdateMembershipStatusCommandHandler(ApplicationDb db)
        {
            this.db = db;
        }

        public override void Execute(UpdateMembershipStatusCommand command, ICommandAndQueryDispatcher dispatcher)
        {
            var getTeamQuery = new GetTeamByIdQuery(command.TeamId);
            Team team = dispatcher.ExecuteQuery(getTeamQuery);
            if (!team.Owner.IsCurrentUser())
            {
                throw new InvalidOperationException("Only the owner of the team can update the membership status");
            }

            TeamMembership teamMembership = team.Members.Single(m => m.User.Id == command.UserIdToUpdate);
            teamMembership.Status = command.NewStatus;
            db.SaveChanges();
        }
    }
}