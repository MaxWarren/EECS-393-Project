using System;
using System.Diagnostics.CodeAnalysis;

namespace ViewModel
{
    /// <summary>
    /// Represents a User
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UserView
    {
        public int UserID { get; set; }
        public string PasswordHash { get; set; }
        public int TeamId { get; set; }
        public UserRole Role { get; set; }
        public string Name { get; set; }

        public UserView(User u)
        {
            UserID = u.User_id;
            PasswordHash = u.Password.Trim();
            TeamId = u.Team_id;
            Role = (UserRole)u.Role.ConvertToInt();
            Name = u.Name.Trim();
        }

        public override bool Equals(object obj)
        {
            UserView other = obj as UserView;
            if (other != null)
            {
                return this.UserID == other.UserID;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.UserID;
        }
    }

    /// <summary>
    /// Represents a Team
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TeamView
    {
        public int TeamID { get; set; }
        public int TeamLeadID { get; set; }
        public int ManagerID { get; set; }
        public string Name { get; set; }

        public TeamView(Team t)
        {
            TeamID = t.Team_id;
            TeamLeadID = t.Team_lead;
            ManagerID = t.Manager;
            Name = t.Team_name.Trim();
        }

        public override bool Equals(object obj)
        {
            TeamView other = obj as TeamView;
            if (other != null)
            {
                return this.TeamID == other.TeamID;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.TeamID;
        }
    }

    /// <summary>
    /// Represents a Project
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ProjectView
    {
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public int OwnerID { get; set; }
        public int TeamID { get; set; }

        public ProjectView(Project p)
        {
            ProjectID = p.Project_id;
            Name = p.Project_name.Trim();
            StartDate = p.Start_date;
            EndDate = p.End_date;
            OwnerID = p.Owner;
            TeamID = p.Team_id;
        }

        public override bool Equals(object obj)
        {
            ProjectView other = obj as ProjectView;
            if (other != null)
            {
                return this.ProjectID == other.ProjectID;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.ProjectID;
        }
    }

    /// <summary>
    /// Represents a Sprint
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SprintView
    {
        public int SprintID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public int ProjectID { get; set; }

        public SprintView(Sprint s)
        {
            SprintID = s.Sprint_id;
            Name = s.Sprint_name.Trim();
            StartDate = s.Start_date;
            EndDate = s.End_date;
            ProjectID = s.Project_id;
        }

        public override bool Equals(object obj)
        {
            SprintView other = obj as SprintView;
            if (other != null)
            {
                return this.SprintID == other.SprintID;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.SprintID;
        }
    }

    /// <summary>
    /// Represents a Story
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class StoryView
    {
        public int StoryID { get; set; }
        public int Priority { get; set; }
        public int SprintID { get; set; }
        public string Text { get; set; }

        public StoryView(Story s)
        {
            StoryID = s.Story_id;
            Priority = s.Priority_num;
            SprintID = s.Sprint_id;
            Text = s.Text;
        }

        public override bool Equals(object obj)
        {
            StoryView other = obj as StoryView;
            if (other != null)
            {
                return this.StoryID == other.StoryID;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.StoryID;
        }
    }

    /// <summary>
    /// Represents a Task
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TaskView
    {
        public int TaskID { get; set; }
        public int StoryID { get; set; }
        public string Text { get; set; }
        public Nullable<int> OwnerID { get; set; }
        public TaskType Type { get; set; }
        public int SizeComplexity { get; set; }
        public int BusinessValue { get; set; }
        public Nullable<DateTime> CompletionDate { get; set; }
        public TaskState State { get; set; }

        public TaskView(Task t)
        {
            TaskID = t.Task_id;
            StoryID = t.Story_id;
            Text = t.Text;
            OwnerID = t.Owner;
            Type = (TaskType)t.Type.ConvertToInt();
            SizeComplexity = t.Size_complexity;
            BusinessValue = t.Business_value;
            CompletionDate = t.Completion_date;
            State = (TaskState)t.State.ConvertToInt();
        }

        public override bool Equals(object obj)
        {
            TaskView other = obj as TaskView;
            if (other != null)
            {
                return this.TaskID == other.TaskID;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.TaskID;
        }
    }
}
