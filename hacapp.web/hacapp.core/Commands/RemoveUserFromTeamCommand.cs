using hacapp.contracts.Commands;

namespace Hacapp.Core.Commands
{
    public class RemoveUserFromTeamCommand : ICommand
    {
        public RemoveUserFromTeamCommand(string userId, int teamId)
        {
            UserId = userId;
            TeamId = teamId;
        }

        public string UserId { get; set; }
        public int TeamId { get; set; }
    }
}