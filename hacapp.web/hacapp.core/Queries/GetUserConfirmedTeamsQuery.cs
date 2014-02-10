using System.Collections.Generic;
using hacapp.contracts.Commands;
using Haccapp.Model.Domain;

namespace Hacapp.Core.Queries
{
    public class GetUserConfirmedTeamsQuery : IQuery<IEnumerable<Team>>
    {
        public GetUserConfirmedTeamsQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; private set; }
    }
}