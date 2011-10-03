using System.Collections.Generic;

namespace DataTypes
{
    /// <summary>
    /// Represents a team
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Unique identifier of this team
        /// </summary>
        public int team_id { get; set; }

        /// <summary>
        /// User who leads this team
        /// </summary>
        public User team_lead { get; set; }

        /// <summary>
        /// User who manages this team
        /// </summary>
        public User manager { get; set; }

        /// <summary>
        /// Name of this team
        /// </summary>
        public string team_name { get; set; }

        /// <summary>
        /// List of all members of this team
        /// </summary>
        public IEnumerable<User> members { get; set; }
    }
}
