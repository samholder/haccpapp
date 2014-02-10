using System.Collections.Generic;
using Hacapp.Web.Models.Identity;

namespace Hacapp.Web.Models.Team
{
    /// <summary>
    ///     View model which represents the details of a team
    /// </summary>
    public class TeamDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UserViewModel Owner { get; set; }

        public bool IsEditable { get; set; }

        public List<UserViewModel> ConfirmedMembers { get; set; }

        public List<UserViewModel> PendingMembers { get; set; }

        public List<UserViewModel> SuspendedMembers { get; set; }
    }
}