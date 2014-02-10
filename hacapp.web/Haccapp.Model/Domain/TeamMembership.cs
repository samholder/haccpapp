using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Haccapp.Model.Identity;

namespace Haccapp.Model.Domain
{
    /// <summary>
    ///     Class representing the status of a member in a team.
    /// </summary>
    public class TeamMembership
    {
        /// <summary>
        ///     Constructor for the EntityFramework
        /// </summary>
        [ExcludeFromCodeCoverage]
        protected internal TeamMembership()
        {
        }

        public TeamMembership(ApplicationUser user, MembershipStatus status)
        {
            User = user;
            Status = status;
        }

        [Required]
        public ApplicationUser User { get; private set; }

        [Required]
        public MembershipStatus Status { get; set; }

        [Required]
        public int Id { get; private set; }
    }
}