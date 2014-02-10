using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Haccapp.Model.Identity;

namespace Haccapp.Model.Domain
{
    /// <summary>
    ///     Represents a team in the domain model
    /// </summary>
    /// <remarks>
    ///     A team is a collection of registered users who will work on the workflows defined for the by the members of
    ///     the team.
    /// </remarks>
    public class Team
    {
        /// <summary>
        ///     Constructor required for the EntityFramework to create the object.
        /// </summary>
        [ExcludeFromCodeCoverage]
        protected internal Team()
        {
            Members = new List<TeamMembership>();
        }

        public Team(string name, ApplicationUser owner)
        {
            Name = name;
            Owner = owner;
            Members = new List<TeamMembership>();
        }

        /// <summary>
        ///     The unique identifier for a team
        /// </summary>
        /// <remarks>The will be a number greater than 0</remarks>
        [Required]
        public int Id { get; private set; }

        /// <summary>
        ///     The name of the team.  This is unique within the owners teams
        /// </summary>
        [Required]
        public string Name { get; private set; }

        /// <summary>
        ///     This is the user who owns the team
        /// </summary>
        [Required]
        public ApplicationUser Owner { get; private set; }

        /// <summary>
        ///     Represents the additional members of the team.  This may be empty if the owner is the only member of the team.
        /// </summary>
        [Required]
        public List<TeamMembership> Members { get; set; }
    }
}