using System.Collections.Generic;
using hacapp.contracts.Commands;
using Haccapp.Model.Domain;

namespace Hacapp.Core.Queries
{
    public class GetUserOwnedTeamsQuery : IQuery<IEnumerable<Team>>
    {
        public GetUserOwnedTeamsQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; private set; }
    }
}