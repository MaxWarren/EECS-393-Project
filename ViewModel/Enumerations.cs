using System;
using System.Collections.Generic;
using System.Data.Linq;

namespace ViewModel
{
    public static class ComplexityValues
    {
        public static IEnumerable<int> businessValue;
        public static IEnumerable<int> sizeComplexity;

        static ComplexityValues()
        {
            businessValue = new List<int>
            {
                0,
                1,
                2,
                3,
                5,
                8,
                13,
                20,
                40,
                80,
                100
            };

            sizeComplexity = new List<int>
            {
                0,
                1,
                2,
                3,
                5,
                8,
                13,
                20,
                40,
                80,
                100
            };
        }
    }

    /// <summary>
    /// Represents user roles as an enum
    /// </summary>
    [Flags]
    public enum UserRole
    {
        None = 0,
        Developer = 1,
        QA = 2,
        Manager = 4,
        Documentation = 8
    }

    /// <summary>
    /// Represents task types as an enum
    /// </summary>
    [Flags]
    public enum TaskType
    {
        None = 0,
        Development = 1,
        QA = 2,
        Documentation = 4
    }

    /// <summary>
    /// Represents task states as an enum
    /// </summary>
    [Flags]
    public enum TaskState
    {
        Unassigned = 0,
        In_Progress = 1,
        Completed = 2,
        Blocked = 4
    }

    /// <summary>
    /// Helper class for converting between TaskState and Binary
    /// </summary>
    public static class TaskStateConverter
    {
        public static Dictionary<string, TaskState> nameMap;

        static TaskStateConverter()
        {
            nameMap = new Dictionary<string, TaskState>()
            {
                {"Unassigned", TaskState.Unassigned},
                {"In Progress", TaskState.In_Progress},
                {"Completed", TaskState.Completed},
                {"Blocked", TaskState.Blocked}
            };
        }

        /// <summary>
        /// Return the Binary object for a given TaskState
        /// </summary>
        /// <param name="state">The TaskState to convert</param>
        /// <returns>A Binary matching the given TaskState</returns>
        public static Binary ConvertToBinary(TaskState state)
        {
            byte[] res = new byte[3];
            res[2] = (byte)state;
            return new Binary(res);
        }
    }

    /// <summary>
    /// Helper class for converting between TaskType and Binary
    /// </summary>
    public static class TaskTypeConverter
    {
        public static Dictionary<string, TaskType> nameMap;

        static TaskTypeConverter()
        {
            nameMap = new Dictionary<string, TaskType>()
            {
                {"Development", TaskType.Development},
                {"Testing", TaskType.QA},
                {"Documentation", TaskType.Documentation}
            };
        }

        /// <summary>
        /// Return the Binary object for a given TaskType
        /// </summary>
        /// <param name="type">The TaskType to convert</param>
        /// <returns>A Binary matching the given TaskType</returns>
        public static Binary ConvertToBinary(TaskType type)
        {
            byte[] res = new byte[3];
            res[2] = (byte)type;
            return new Binary(res);
        }
    }

    /// <summary>
    /// Helper class for converting between UserRole and Binary
    /// </summary>
    public static class UserRoleConverter
    {
        public static Dictionary<string, UserRole> nameMap;

        static UserRoleConverter()
        {
            nameMap = new Dictionary<string, UserRole>()
            {
                {"Developer", UserRole.Developer},
                {"Quality Assurance", UserRole.QA},
                {"Manager", UserRole.Manager},
                {"Documentation", UserRole.Documentation}
            };
        }

        /// <summary>
        /// Return the Binary object for a given UserRole
        /// </summary>
        /// <param name="role">The UserRole to convert</param>
        /// <returns>A Binary matching the given UserRole</returns>
        public static Binary ConvertToBinary(UserRole role)
        {
            byte[] res = new byte[3];
            res[2] = (byte)role;
            return new Binary(res);
        }
    }
}
