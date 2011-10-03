using System;
using System.Collections.Generic;

namespace DataTypes
{
    /// <summary>
    /// Represents a Project
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Unique id of this project
        /// </summary>
        public int project_id { get; set; }

        /// <summary>
        /// Name of this project
        /// </summary>
        public string project_name { get; set; }

        /// <summary>
        /// Start date of this project
        /// </summary>
        public DateTime start_date { get; set; }

        /// <summary>
        /// End date of this project
        /// </summary>
        public DateTime end_date { get; set; }

        /// <summary>
        /// Manager who owns this project
        /// </summary>
        public User owner { get; set; }

        /// <summary>
        /// The team responsible for this proejct
        /// </summary>
        public Team team { get; set; }

        /// <summary>
        /// List of all sprints in this project
        /// </summary>
        public IEnumerable<Sprint> sprints { get; set; }
    }
}
