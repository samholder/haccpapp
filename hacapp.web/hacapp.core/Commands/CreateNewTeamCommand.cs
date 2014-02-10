using hacapp.contracts.Commands;
using Haccapp.Model.Identity;

namespace Hacapp.Core.Commands
{
    /// <summary>
    ///     Command which encapsulates the data required to create a new command
    /// </summary>
    public class CreateNewTeamCommand : ICommand
    {
        public CreateNewTeamCommand(string teamName, ApplicationUser teamOwnerId)
        {
            TeamName = new Optional<string>(teamName);
            TeamOwnerId = teamOwnerId;
        }

        /// <summary>
        ///     Constructor for model binder to use
        /// </summary>
        public CreateNewTeamCommand()
        {
        }

        public Optional<string> TeamName { get; private set; }

        public int TeamId { get; set; }

        public ApplicationUser TeamOwnerId { get; set; }
    }
}