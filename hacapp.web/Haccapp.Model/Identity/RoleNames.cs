using System;

namespace Haccapp.Model.Identity
{
    /// <summary>
    ///     Class which holds the definitions of the rolenames in the system.
    /// </summary>
    /// <remarks>
    ///     Role names are held in a way that makes them strongly typed but also implicitly convertible to strings for
    ///     ease of use.
    /// </remarks>
    public class RoleName
    {
        private const string TeamManagementIdentifier = "TeamManagement";
        private static readonly RoleName TeamManagementInstance = new RoleName(TeamManagementIdentifier);
        private readonly string name;

        private RoleName(string name)
        {
            this.name = name;
        }

        public static RoleName TeamManagement
        {
            get { return TeamManagementInstance; }
        }

        public static implicit operator string(RoleName roleName)
        {
            return roleName.ToString();
        }

        public override string ToString()
        {
            return name;
        }

        /// <summary>
        ///     Attempts to create a RoleName instance from the given string.
        /// </summary>
        /// <param name="roleName">
        ///     The name of the role to parse into a RoleName instance. This must map to a known role in the
        ///     system.
        /// </param>
        /// <returns>
        ///     A RoleName instance, which will be the static instance of the RoleName which has the same idfentifier as the
        ///     given roleName.
        /// </returns>
        public RoleName Parse(string roleName)
        {
            switch (roleName)
            {
                case TeamManagementIdentifier:
                    return TeamManagement;
                default:
                    throw new ArgumentException(
                        string.Format("The given roleName '{0}' did not map to a known rolename in the system.",
                            roleName));
            }
        }
    }
}