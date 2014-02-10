using hacapp.contracts.Commands;
using Haccapp.Model.Domain;

namespace Hacapp.Core.Commands
{
    public class UpdateMembershipStatusCommand : ICommand
    {
        public UpdateMembershipStatusCommand(string userIdToUpdate, int teamId, MembershipStatus newStatus)
        {
            UserIdToUpdate = userIdToUpdate;
            TeamId = teamId;
            NewStatus = newStatus;
        }

        public string UserIdToUpdate { get; private set; }
        public int TeamId { get; private set; }
        public MembershipStatus NewStatus { get; private set; }
    }
}