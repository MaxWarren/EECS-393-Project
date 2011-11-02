using System;
using DatabaseLayer;

namespace ViewModel
{
    /// <summary>
    /// Represents a User
    /// </summary>
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
    }

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
    }

    /// <summary>
    /// Represents a Project
    /// </summary>
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
    }

    /// <summary>
    /// Represents a Sprint
    /// </summary>
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
    }

    /// <summary>
    /// Represents a Story
    /// </summary>
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
    }
    
    /// <summary>
    /// Represents a Task
    /// </summary>
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
    }
}
