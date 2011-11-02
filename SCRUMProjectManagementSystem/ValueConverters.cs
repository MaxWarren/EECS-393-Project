using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using ViewModel;

namespace SCRUMProjectManagementSystem
{
    public class TaskStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object paramter, System.Globalization.CultureInfo culture)
        {
            if (value is TaskState)
            {
                TaskState state = (TaskState)value;
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
                        return "None";
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object paramter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                string state = (string)value;
                switch (state)
                {
                    case "Unassigned":
                        return TaskState.Unassigned;
                    case "In Progress":
                        return TaskState.In_Progress;
                    case "Completed":
                        return TaskState.Completed;
                    case "Blocked":
                        return TaskState.Blocked;
                    default:
                        return TaskState.Unassigned;
                }
            }

            return TaskState.Unassigned;
        }
    }

    public class TaskTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object paramter, System.Globalization.CultureInfo culture)
        {
            if (value is TaskType)
            {
                TaskType type = (TaskType)value;

                switch (type)
                {
                    case TaskType.Development:
                        return "Development";
                    case TaskType.QA:
                        return "Testing";
                    case TaskType.Documentation:
                        return "Documentation";
                    default:
                        return "None";
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object paramter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                string type = (string)value;

                switch (type)
                {
                    case "Testing":
                        return TaskType.QA;
                    case "Documentation":
                        return TaskType.Documentation;
                    default:
                        return TaskType.Development;
                }
            }

            return TaskType.Development;
        }
    }
}
