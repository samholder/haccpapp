using System.Collections.Generic;
using System.Linq;
using hacapp.contracts.Commands;
using Hacapp.Core.Queries;
using Hacapp.Core.Queries.Result;
using Hacapp.Data.DataContexts;
using Haccapp.Model.Domain;

namespace Hacapp.Data.Query
{
    public class GetPendingMembershipsForUsersTeamsQueryHandler :
        QueryHandlerBase<GetPendingMembershipsForUsersTeamsQuery, IEnumerable<PendingMembershipResult>>
    {
        private readonly ApplicationDb db;

        public GetPendingMembershipsForUsersTeamsQueryHandler(ApplicationDb db)
        {
            this.db = db;
        }

        public override IEnumerable<PendingMembershipResult> Execute(GetPendingMembershipsForUsersTeamsQuery query,
            ICommandAndQueryDispatcher dispatcher)
        {
            var pendingMemberships =
                db.Teams.Where(
                    t => t.Owner.Id == query.UserId && t.Members.Any(m => m.Status == MembershipStatus.Pending))
                    .SelectMany(
                        t =>
                            t.Members.Where(m => m.Status == MembershipStatus.Pending)
                                .Select(m => new {UserId = m.User.Id, TeamId = t.Id, m.User.UserName, TeamName = t.Name}))
                    .ToList();

            return
                pendingMemberships.Select(
                    pendingMembership =>
                        new PendingMembershipResult
                        {
                            UserId = pendingMembership.UserId,
                            TeamId = pendingMembership.TeamId,
                            UserName = pendingMembership.UserName,
                            TeamName = pendingMembership.TeamName
                        }).ToList();
        }
    }
}