using System.Collections.Generic;

namespace DataTypes
{
    /// <summary>
    /// Represents a user story
    /// </summary>
    class UserStory
    {
        /// <summary>
        /// Unique identifier of this story
        /// </summary>
        public int story_id { get; set; }

        /// <summary>
        /// Priority value of this story
        /// </summary>
        public int priority_num { get; set; }

        /// <summary>
        /// Sprint to which this story belongs
        /// </summary>
        public Sprint sprint { get; set; }

        /// <summary>
        /// Text of this story
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// List of all tasks belonging to this story
        /// </summary>
        public IEnumerable<Task> tasks { get; set; }
    }
}
