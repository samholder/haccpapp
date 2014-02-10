using System.Collections.Generic;
using hacapp.contracts.Commands;
using Hacapp.Core.Queries.Result;

namespace Hacapp.Core.Queries
{
    public class GetPendingMembershipsForUsersTeamsQuery : IQuery<IEnumerable<PendingMembershipResult>>
    {
        public GetPendingMembershipsForUsersTeamsQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; private set; }
    }
}