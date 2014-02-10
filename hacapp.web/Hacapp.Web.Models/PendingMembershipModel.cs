namespace Hacapp.Web.Models
{
    /// <summary>
    ///     Model which represents a users pending membership to a team.
    /// </summary>
    public class PendingMembershipModel
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