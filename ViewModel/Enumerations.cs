using System;
using System.Collections.Generic;
using System.Data.Linq;

namespace ViewModel
{
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
        /// <summary>
        /// Return the Binary object for a given TaskState
        /// </summary>
        /// <param name="state">The TaskState to convert</param>
        /// <returns>A Binary matching the given TaskState</returns>
        public static Binary ConvertToBinary(TaskState state)
        {
            return new Binary(BitConverter.GetBytes((int)state));
        }

        /// <summary>
        /// Converts a TaskState to a string
        /// </summary>
        /// <param name="role">The TaskState to convert</param>
        /// <returns>A string matching the given TaskState</returns>
        public static string ConvertToString(TaskState state)
        {
            switch (state)
            {
                case TaskState.Unassigned:
                    return "Unassigned";
                case TaskState.In_Progress:
                    return "In Progress";
                case TaskState.Completed:
                    return "Completed";
                case TaskState.Blocked:
                    return "Blocked";
                default:
                    return "Invalid State";
            }
        }

        /// <summary>
        /// Converts a Binary to a TaskState
        /// </summary>
        /// <param name="bin">The Binary to convert</param>
        /// <returns>The TaskState matching the given binary</returns>
        public static TaskState ConvertBinaryToState(Binary bin)
        {
            return (TaskState)BitConverter.ToInt16(bin.ToArray(), 1);
        }

        /// <summary>
        /// Converts a Binary to a string
        /// </summary>
        /// <param name="bin">The Binary to convert</param>
        /// <returns>A string matching the given binary</returns>
        public static String ConvertBinaryToString(Binary bin)
        {
            return ConvertToString(ConvertBinaryToState(bin));
        }
    }

    /// <summary>
    /// Helper class for converting between TaskType and Binary
    /// </summary>
    public static class TaskTypeConverter
    {
        /// <summary>
        /// Return the Binary object for a given TaskType
        /// </summary>
        /// <param name="type">The TaskType to convert</param>
        /// <returns>A Binary matching the given TaskType</returns>
        public static Binary ConvertToBinary(TaskType type)
        {
            return new Binary(BitConverter.GetBytes((int)type));
        }

        /// <summary>
        /// Converts a TaskType to a string
        /// </summary>
        /// <param name="role">The TaskType to convert</param>
        /// <returns>A string matching the given TaskType</returns>
        public static string ConvertToString(TaskType type)
        {
            switch (type)
            {
                case TaskType.Development:
                    return "Development";
                case TaskType.QA:
                    return "Testing";
                case TaskType.Documentation:
                    return "Documentation";
                default:
                    return "Invalid Type";
            }
        }

        /// <summary>
        /// Converts a Binary to a TaskType
        /// </summary>
        /// <param name="bin">The Binary to convert</param>
        /// <returns>The TaskType matching the given binary</returns>
        public static TaskType ConvertBinaryToType(Binary bin)
        {
            return (TaskType)BitConverter.ToInt16(bin.ToArray(), 1);
        }

        /// <summary>
        /// Converts a Binary to a string
        /// </summary>
        /// <param name="bin">The Binary to convert</param>
        /// <returns>A string matching the given binary</returns>
        public static String ConvertBinaryToString(Binary bin)
        {
            return ConvertToString(ConvertBinaryToType(bin));
        }
    }

    /// <summary>
    /// Helper class for converting between UserRole and Binary
    /// </summary>
    public static class UserRoleConverter
    {
        /// <summary>
        /// Return the Binary object for a given UserRole
        /// </summary>
        /// <param name="role">The UserRole to convert</param>
        /// <returns>A Binary matching the given UserRole</returns>
        public static Binary ConvertToBinary(UserRole role)
        {
            return new Binary(BitConverter.GetBytes((int)role));
        }

        /// <summary>
        /// Converts a UserRole to a string
        /// </summary>
        /// <param name="role">The UserRole to convert</param>
        /// <returns>A string matching the given UserRole</returns>
        public static string ConvertToString(UserRole role)
        {
            switch (role)
            {
                case UserRole.Developer:
                    return "Developer";
                case UserRole.QA:
                    return "Quality Assurance";
                case UserRole.Manager:
                    return "Manager";
                case UserRole.Documentation:
                    return "Documentation";
                default:
                    return "Invalid Role";
            }
        }

        /// <summary>
        /// Converts a Binary to a UserRole
        /// </summary>
        /// <param name="bin">The Binary to convert</param>
        /// <returns>The UserRole matching the given binary</returns>
        public static UserRole ConvertBinaryToRole(Binary bin)
        {
            Console.WriteLine(bin.ToArray());
            return (UserRole)BitConverter.ToInt16(bin.ToArray(), 1);
        }

        /// <summary>
        /// Converts a Binary to a string
        /// </summary>
        /// <param name="bin">The Binary to convert</param>
        /// <returns>A string matching the given binary</returns>
        public static String ConvertBinaryToString(Binary bin)
        {
            return ConvertToString(ConvertBinaryToRole(bin));
        }
    }
}
