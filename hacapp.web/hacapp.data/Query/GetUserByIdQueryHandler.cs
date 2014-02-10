using hacapp.contracts.Commands;
using Hacapp.Core.Queries;
using Hacapp.Data.DataContexts;
using Haccapp.Model;
using Haccapp.Model.Identity;

namespace Hacapp.Data.Query
{
    public class GetUserByIdQueryHandler : QueryHandlerBase<GetUserByIdQuery, ApplicationUser>
    {
        private readonly ApplicationDb db;

        public GetUserByIdQueryHandler(ApplicationDb db)
        {
            this.db = db;
        }

        public override ApplicationUser Execute(GetUserByIdQuery query, ICommandAndQueryDispatcher dispatcher)
        {
            return db.Users.Find(query.GetUserId);
        }
    }
}