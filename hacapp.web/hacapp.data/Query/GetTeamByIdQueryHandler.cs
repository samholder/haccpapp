using System.Data.Entity;
using System.Linq;
using hacapp.contracts.Commands;
using Hacapp.Core.Commands;
using Hacapp.Core.Queries;
using Hacapp.Data.DataContexts;
using Haccapp.Model.Domain;

namespace Hacapp.Data.Query
{
    public class GetTeamByIdQueryHandler : QueryHandlerBase<GetTeamByIdQuery, Team>
    {
        private readonly ApplicationDb db;

        public GetTeamByIdQueryHandler(ApplicationDb db)
        {
            this.db = db;
        }

        public override Team Execute(GetTeamByIdQuery query, ICommandAndQueryDispatcher dispatcher)
        {
            Team team = db.Teams
                .Include(x => x.Owner)
                .Include(x => x.Members.Select(m => m.User))
                .FirstOrDefault(t => t.Id == query.TeamId);
            if (team == null)
            {
                throw new CommandExecutionException(CommandExecutionExceptionMessages.TeamDoesNotExist(query.TeamId));
            }
            return team;
        }
    }
}