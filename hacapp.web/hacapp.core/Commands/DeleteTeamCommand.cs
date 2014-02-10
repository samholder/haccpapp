using hacapp.contracts.Commands;

namespace Hacapp.Core.Commands
{
    public class DeleteTeamCommand : ICommand
    {
        public DeleteTeamCommand(int teamId)
        {
            TeamId = teamId;
        }

        public int TeamId { get; private set; }
    }
}