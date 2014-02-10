using hacapp.contracts.Commands;
using Haccapp.Model.Domain;

namespace Hacapp.Core.Queries
{
    public class GetTeamByIdQuery : IQuery<Team>
    {
        public GetTeamByIdQuery(int teamId)
        {
            TeamId = teamId;
        }

        public int TeamId { get; private set; }
    }
}