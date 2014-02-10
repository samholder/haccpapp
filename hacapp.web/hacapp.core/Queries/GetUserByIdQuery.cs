using hacapp.contracts.Commands;
using Haccapp.Model.Identity;

namespace Hacapp.Core.Queries
{
    public class GetUserByIdQuery : IQuery<ApplicationUser>
    {
        public GetUserByIdQuery(string getUserId)
        {
            GetUserId = getUserId;
        }

        public string GetUserId { get; private set; }
    }
}