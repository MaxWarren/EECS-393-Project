using System;
using System.Collections.Generic;

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
