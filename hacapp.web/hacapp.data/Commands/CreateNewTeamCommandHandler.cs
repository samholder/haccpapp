using System;
using System.Linq;
using hacapp.contracts.Commands;
using Hacapp.Core.Commands;
using Hacapp.Core.Queries;
using Hacapp.Data.DataContexts;
using Haccapp.Model.Domain;

namespace Hacapp.Data.Commands
{
    /// <summary>
    ///     Handler for creating the creating new team command, which will add the team to table storage and associated
    /// </summary>
    public class CreateNewTeamCommandHandler : CommandHandlerBase<CreateNewTeamCommand>
    {
        private readonly ApplicationDb db;

        public CreateNewTeamCommandHandler(ApplicationDb db)
        {
            this.db = db;
        }

        public override void Execute(CreateNewTeamCommand command, ICommandAndQueryDispatcher dispatcher)
        {
            if (!command.TeamName.HasValue)
            {
                throw new ArgumentException("The team name was invalid, it cannot be blank.");
            }

            var teamExistsQuery = new GetUserOwnedTeamsQuery(command.TeamOwnerId.Id);
            if (dispatcher.ExecuteQuery(teamExistsQuery).Any(t => t.Name == command.TeamName.Value))
            {
                //team with this name already exists, so we can't add a new one
                throw new ArgumentException(
                    string.Format("The team name was invalid.  A team called '{0}' already exists.",
                        command.TeamName.Value));
            }
            var team = new Team(command.TeamName.Value, command.TeamOwnerId);
            db.Teams.Add(team);
            db.SaveChanges();
            command.TeamId = team.Id;
        }
    }
}