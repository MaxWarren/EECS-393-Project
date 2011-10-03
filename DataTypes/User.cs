namespace DataTypes
{
    /// <summary>
    /// Represents a user of this system
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier of this user
        /// </summary>
        public int user_id { get; set; }

        /// <summary>
        /// Password of this user
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// Team to which this user belongs
        /// </summary>
        public Team team { get; set; }

        /// <summary>
        /// Organizational role of this user
        /// </summary>
        public UserRole role { get; set; }

        /// <summary>
        /// Name of this user
        /// </summary>
        public string name { get; set; }
    }
}
