using System.Collections.Generic;
using hacapp.contracts.Commands;
using Haccapp.Model.Domain;

namespace Hacapp.Core.Queries
{
    public class GetTeamsUserDoesNotBelongToQuery : IQuery<IEnumerable<Team>>
    {
        public GetTeamsUserDoesNotBelongToQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; private set; }
    }

    
}