using hacapp.contracts.Commands;

namespace Hacapp.Core.Commands
{
    public class JoinTeamCommand : ICommand
    {
        public JoinTeamCommand(int teamId, string getUserId)
        {
            TeamId = teamId;
            GetUserId = getUserId;
        }

        public int TeamId { get; private set; }
        public string GetUserId { get; private set; }
    }
}