using System.Collections.Generic;
using hacapp.contracts.Commands;
using Haccapp.Model.Domain;

namespace Hacapp.Core.Queries
{
    public class GetUserTeamsQuery : IQuery<IEnumerable<Team>>
    {
        public GetUserTeamsQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; private set; }
    }
}