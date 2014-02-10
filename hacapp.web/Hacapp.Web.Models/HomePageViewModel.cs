using System.Collections.Generic;

namespace Hacapp.Web.Models
{
    /// <summary>
    ///     Model that contains all of the data that will be used on the homepage
    /// </summary>
    public class HomePageViewModel
    {
        /// <summary>
        ///     Collection of pending memberships for the teams that the current logged in user is the owner of.
        /// </summary>
        public List<PendingMembershipModel> PendingMemberships { get; set; }
    }
}