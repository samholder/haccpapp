using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using hacapp.contracts.Commands;
using Hacapp.Core.Queries;
using Hacapp.Data.DataContexts;
using Haccapp.Model.Domain;

namespace Hacapp.Data.Query
{
    public class GetAllTeamsQueryHandler : QueryHandlerBase<GetAllTeamsQuery, IEnumerable<Team>>
    {
        private readonly ApplicationDb db;

        public GetAllTeamsQueryHandler(ApplicationDb db)
        {
            this.db = db;
        }

        public override IEnumerable<Team> Execute(GetAllTeamsQuery query, ICommandAndQueryDispatcher dispatcher)
        {
            return db.Teams
                .Include(x => x.Owner)
                .Include(x => x.Members.Select(m => m.User))
                .ToList();
        }
    }
}