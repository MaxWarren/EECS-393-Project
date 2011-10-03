using System;
using System.Collections.Generic;

namespace DataTypes
{
    /// <summary>
    /// Represents a sprint
    /// </summary>
    public class Sprint
    {
        /// <summary>
        /// Unique indentifier of this sprint
        /// </summary>
        public int sprint_id { get; set; }

        /// <summary>
        /// Name of this sprint
        /// </summary>
        public string sprint_name { get; set; }

        /// <summary>
        /// Start date of this sprint
        /// </summary>
        public DateTime start_date { get; set; }

        /// <summary>
        /// End date of this sprint
        /// </summary>
        public DateTime end_date { get; set; }

        /// <summary>
        /// Project to which this sprint belongs
        /// </summary>
        public Project project { get; set; }

        /// <summary>
        /// List of all user stories belonging to this sprint
        /// </summary>
        public IEnumerable<UserStory> user_stories { get; set; }
    }
}
