using System;

namespace DataTypes
{
    /// <summary>
    /// Represents a task
    /// </summary>
    class Task
    {
        /// <summary>
        /// Unique identifier of this task
        /// </summary>
        public int task_id { get; set; }

        /// <summary>
        /// User story to which this task belongs
        /// </summary>
        public UserStory story { get; set; }

        /// <summary>
        /// Text of this task
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// User responsible for completing this task
        /// </summary>
        public User owner { get; set; }

        /// <summary>
        /// Type of this task
        /// </summary>
        public TaskType type { get; set; }
        
        /// <summary>
        /// Size complexity of this task
        /// </summary>
        public int size_complexity { get; set; }

        /// <summary>
        /// Business value of this task
        /// </summary>
        public int business_value { get; set; }

        /// <summary>
        /// Date this task was completed
        /// </summary>
        public DateTime completion_date { get; set; }

        /// <summary>
        /// Current state of this task
        /// </summary>
        public TaskState state { get; set; }
    }
}
