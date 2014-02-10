using System.Collections.Generic;
using hacapp.contracts.Commands;
using Haccapp.Model.Domain;

namespace Hacapp.Core.Queries
{
    public class GetAllTeamsQuery : IQuery<IEnumerable<Team>>
    {
    }
}