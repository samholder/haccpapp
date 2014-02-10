namespace Hacapp.Core.Queries.Result
{
    public class PendingMembershipResult
    {
        /// <summary>
        ///     The id if the user that has the membership pending
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///     The display name for the user who has a pending membership
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     The id of the team that the user has the pending membership for
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        ///     The display name for the team that the user has a pending membership for
        /// </summary>
        public string TeamName { get; set; }
    }
}