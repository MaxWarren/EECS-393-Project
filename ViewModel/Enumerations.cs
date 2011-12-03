using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ViewModel
{
    public static class EnumValues
    {
        public static IEnumerable<int> businessValue;
        public static IEnumerable<int> sizeComplexity;
        public static IEnumerable<TaskState> taskState;
        public static IEnumerable<TaskType> taskType;

        [ExcludeFromCodeCoverage]
        static EnumValues()
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

            taskState = new List<TaskState>
            {
                TaskState.Unassigned,
                TaskState.In_Progress,
                TaskState.Completed,
                TaskState.Blocked
            };

            taskType = new List<TaskType>
            {
                TaskType.Development,
                TaskType.Documentation,
                TaskType.QA
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
}
