using System;

namespace DataTypes
{
    /// <summary>
    /// Represents a user's organizational role
    /// </summary>
    [Flags]
    public enum UserRole
    {
        JustResting = 0, // He's pining for the fjords
        Developer = 1,
        QA = 2,
        Manager = 4,
        Documentation = 8
    }

    /// <summary>
    /// Represents the current state of a task
    /// </summary>
    [Flags]
    public enum TaskState
    {
        MissingInAction = 0, // Invalid state
        New = 1,
        InProgress = 2,
        Complete = 4,
        Blocked = 8
    }

    /// <summary>
    /// Represents the type of a task
    /// </summary>
    [Flags]
    public enum TaskType
    {
        TopSecret = 0, // Invalid state
        Development = 1,
        QA = 2,
        Documentation = 4,
        Administrative = 8
    }
}