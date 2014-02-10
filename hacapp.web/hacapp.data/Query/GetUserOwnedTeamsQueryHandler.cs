using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using hacapp.contracts.Commands;
using Hacapp.Core.Queries;
using Hacapp.Data.DataContexts;
using Haccapp.Model.Domain;

namespace Hacapp.Data.Query
{
    public class GetUserOwnedTeamsQueryHandler : QueryHandlerBase<GetUserOwnedTeamsQuery, IEnumerable<Team>>
    {
        private readonly ApplicationDb db;

        public GetUserOwnedTeamsQueryHandler(ApplicationDb db)
        {
            this.db = db;
        }

        public override IEnumerable<Team> Execute(GetUserOwnedTeamsQuery query, ICommandAndQueryDispatcher dispatcher)
        {
            return db.Teams
                .Include(x => x.Owner)
                .Include(x => x.Members.Select(m => m.User))
                .Where(x => x.Owner.Id == query.UserId).ToList();
        }
    }
}