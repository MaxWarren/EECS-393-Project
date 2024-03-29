﻿using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Data.SqlClient;

namespace DatabaseLayer
{
    /// <summary>
    /// Class that interfaces with the database
    /// </summary>
    public class DataModel : IDataModel
    {
        #region Fields and Constructors
        /// <summary>
        /// Connection to the database
        /// </summary>
        private Eecs393_project dbConnection;

        /// <summary>
        /// Connection string for connecting to the database
        /// </summary>
        private string connString;

        /// <summary>
        /// The single instance of this class
        /// </summary>
        public static readonly DataModel Instance = new DataModel();

        /// <summary>
        /// Empty static constructor to prevent compiler from marking with beforefieldinit
        /// DO NOT REMOVE
        /// </summary>
        static DataModel() { }

        /// <summary>
        /// Initialize the database connection
        /// </summary>
        private DataModel()
        {
            // Set connection string
            connString = string.Format("user id={0};password={1};server={2};Trusted_Connection=no;database={3};connection timeout={4}",
                Properties.Settings.Default.user_id,
                Properties.Settings.Default.password,
                Properties.Settings.Default.server,
                Properties.Settings.Default.database,
                Properties.Settings.Default.timeout);

            dbConnection = new Eecs393_project(connString);
        }
        #endregion

        #region Get Projects
        /// <summary>
        /// Get all projects in the database
        /// </summary>
        /// <returns>A list of all projects in the database</returns>
        public IEnumerable<Project> GetAllProjects()
        {
            try
            {
                IEnumerable<Project> projects = from p in dbConnection.Project
                                                orderby p.Project_name ascending
                                                select p;
                return projects;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all projects belonging to a specific team
        /// </summary>
        /// <param name="teamId">The ID of the team for which to search</param>
        /// <returns>A list of all projects belonging to the given team</returns>
        public IEnumerable<Project> GetProjectsByTeam(int teamId)
        {
            try
            {
                IEnumerable<Project> projects = from p in dbConnection.Project
                                                where p.Team_id == teamId
                                                orderby p.Project_name ascending
                                                select p;
                return projects;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all projects owned by a specific user
        /// </summary>
        /// <param name="ownerID">The ID of the user for whom to search</param>
        /// <returns>A list of all projects owned by the given user</returns>
        public IEnumerable<Project> GetProjectsByOwner(int ownerID)
        {
            try
            {
                IEnumerable<Project> projects = from p in dbConnection.Project
                                                where p.Owner == ownerID
                                                where (!p.End_date.HasValue || p.End_date <= DateTime.Today)
                                                orderby p.Project_name ascending
                                                select p;
                return projects;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the project with the given ID
        /// </summary>
        /// <param name="projectID">The project ID for which to search</param>
        /// <returns>The project with the given ID</returns>
        public Project GetProjectByID(int projectID)
        {
            try
            {
                IEnumerable<Project> projects = from p in dbConnection.Project
                                                where p.Project_id == projectID
                                                select p;

                return projects.First(); // There can be only one
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get Sprints
        /// <summary>
        /// Gets all sprints for a project
        /// </summary>
        /// <param name="projectID">The project for which to retrieve sprints</param>
        /// <returns>A list of all sprints for the given project</returns>
        public IEnumerable<Sprint> GetSprintsForProject(int projectID)
        {
            try
            {
                IEnumerable<Sprint> sprints = from s in dbConnection.Sprint
                                              where s.Project_id == projectID
                                              orderby s.Start_date descending, (s.Sprint_name.Equals("Backlog")) ascending
                                              select s;

                return sprints;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the backlog for a project
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public Sprint GetProjectBacklog(int projectID)
        {
            try
            {
                IEnumerable<Sprint> sprints = from s in dbConnection.Sprint
                                              where s.Project_id == projectID
                                              where s.Sprint_name.Equals("Backlog")
                                              select s;

                return sprints.First(); // There can be only one
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the sprint with the given ID
        /// </summary>
        /// <param name="sprintID">The sprint ID for which to search</param>
        /// <returns>The sprint with the given ID</returns>
        public Sprint GetSprintByID(int sprintID)
        {
            try
            {
                IEnumerable<Sprint> sprints = from s in dbConnection.Sprint
                                              where s.Sprint_id == sprintID
                                              select s;

                return sprints.First(); // There can be only one
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get Stories
        /// <summary>
        /// Gets all user stories assigned to a sprint
        /// </summary>
        /// <param name="sprintID">The ID of the sprint for which to search</param>
        /// <returns>A list of all user stories assigned to the given sprint</returns>
        public IEnumerable<Story> GetStoriesForSprint(int sprintID)
        {
            try
            {
                IEnumerable<Story> stories = from s in dbConnection.Story
                                             where s.Sprint_id == sprintID
                                             orderby s.Priority_num ascending
                                             select s;

                return stories;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a user story with the given ID
        /// </summary>
        /// <param name="storyID">The ID of the story for which to search</param>
        /// <returns>The story with the given ID</returns>
        public Story GetStoryByID(int storyID)
        {
            try
            {
                IEnumerable<Story> stories = from s in dbConnection.Story
                                             where s.Story_id == storyID
                                             select s;

                return stories.First(); // There can be only one
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get Tasks
        /// <summary>
        /// Gets all tasks in the database
        /// </summary>
        /// <returns>A list of all tasks in the database</returns>
        public IEnumerable<Task> GetAllTasks()
        {
            try
            {
                IEnumerable<Task> tasks = from t in dbConnection.Task
                                          select t;

                return tasks;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all tasks for a given user story
        /// </summary>
        /// <param name="storyID">The ID of the story for which to search</param>
        /// <returns>A list of all tasks for the given user story</returns>
        public IEnumerable<Task> GetTasksForStory(int storyID)
        {
            try
            {
                IEnumerable<Task> tasks = from t in dbConnection.Task
                                          where t.Story_id == storyID
                                          orderby t.Business_value descending
                                          select t;

                return tasks;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all tasks for a given sprint
        /// </summary>
        /// <param name="sprintID">The ID of the sprint for which to search</param>
        /// <returns>A list of all taks for the given sprint</returns>
        public IEnumerable<Task> GetAllTasksForSprint(int sprintID)
        {
            try
            {
                IEnumerable<Task> tasks = from t in dbConnection.Task
                                          where t.Story.Sprint_id == sprintID
                                          select t;

                return tasks;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all tasks owned by a given user
        /// </summary>
        /// <param name="userID">The ID of the user for which to search</param>
        /// <returns>A list of all tasks belonging to the given user</returns>
        public IEnumerable<Task> GetTasksForUser(int userID)
        {
            try
            {
                IEnumerable<Task> tasks = from t in dbConnection.Task
                                          where t.Owner == userID
                                          where !t.Completion_date.HasValue
                                          orderby t.Business_value descending
                                          select t;

                return tasks;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a task with the given ID
        /// </summary>
        /// <param name="taskID">The ID of the task for which to search</param>
        /// <returns>The task with the given ID</returns>
        public Task GetTaskByID(int taskID)
        {
            try
            {
                IEnumerable<Task> tasks = from t in dbConnection.Task
                                          where t.Task_id == taskID
                                          select t;

                return tasks.First(); // There can be only one
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get Teams
        /// <summary>
        /// Gets all teams in the database
        /// </summary>
        /// <returns>A list of all teams in the database</returns>
        public IEnumerable<Team> GetAllTeams()
        {
            try
            {
                IEnumerable<Team> teams = from t in dbConnection.Team
                                          orderby t.Team_name ascending
                                          select t;

                return teams;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the team with the given ID
        /// </summary>
        /// <param name="teamID">The ID of the team for which to search</param>
        /// <returns>The team with the given ID</returns>
        public Team GetTeamByID(int teamID)
        {
            try
            {
                IEnumerable<Team> teams = from t in dbConnection.Team
                                          where t.Team_id == teamID
                                          select t;

                return teams.First(); // There can be only one
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get Users
        /// <summary>
        /// Gets the user with the given ID
        /// </summary>
        /// <param name="userID">The ID of the user for which to search</param>
        /// <returns>The user with the given ID</returns>
        public User GetUserByID(int userID)
        {
            try
            {
                IEnumerable<User> users = from u in dbConnection.User
                                          where u.User_id == userID
                                          select u;

                return users.First(); // There can be only one
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all users in the database
        /// </summary>
        /// <returns>A list of all users in the database</returns>
        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                IEnumerable<User> users = from u in dbConnection.User
                                          orderby u.User_id ascending
                                          select u;

                return users;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all members of a team
        /// </summary>
        /// <param name="teamID">The ID of the team for which to search</param>
        /// <returns>A list of all members of the given team</returns>
        public IEnumerable<User> GetTeamMembers(int teamID)
        {
            try
            {
                IEnumerable<User> users = from u in dbConnection.User
                                          where u.Team_id == teamID
                                          select u;

                return users;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all Users not in a team
        /// </summary>
        /// <param name="teamID">The ID of the team for which to search</param>
        /// <returns>A list of all Users not in the given team</returns>
        public IEnumerable<User> GetUsersNotInTeam(int teamID)
        {
            try
            {
                IEnumerable<User> users = from u in dbConnection.User
                                          where u.Team_id != teamID
                                          select u;

                return users;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Authenticates a user
        /// </summary>
        /// <param name="userID">The ID of the user to authenticate</param>
        /// <param name="password">The password provided by the user</param>
        /// <returns>The User if authentication succeeded, false otherwise</returns>
        public User AuthenticateUser(int userID, string password)
        {
            try
            {
                IEnumerable<User> users = from u in dbConnection.User
                                          where u.User_id == userID
                                          where u.Password == password
                                          select u;

                return users.First(); // Are the any users with the given ID and password
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Create New Entities
        /// <summary>
        /// Creates a new team
        /// </summary>
        /// <param name="name">The name of the team</param>
        /// <param name="managerID">The ID of the team's manager</param>
        /// <param name="leadID">The ID of the team lead</param>
        /// <returns>True if creating the team succeeds, false otherwise</returns>
        public bool CreateTeam(string name, int managerID, int leadID)
        {
            User managerUser = GetUserByID(managerID);
            User leadUser = GetUserByID(leadID);

            if (managerUser == null || leadUser == null)
            {
                return false;
            }

            Team newTeam = new Team()
            {
                Manager = managerID,
                ManagerUser = managerUser,
                Project = new System.Data.Linq.EntitySet<Project>(),
                Team_ = new System.Data.Linq.EntitySet<User>() { leadUser },
                Team_lead = leadID,
                Team_name = name,
                User = leadUser
            };

            return commitChanges();
        }

        /// <summary>
        /// Creates a new project
        /// </summary>
        /// <param name="name">The name of the project</param>
        /// <param name="startDate">The start date of the project</param>
        /// <param name="endDate">The end date of the project if it exists</param>
        /// <param name="ownerID">The ID of the project owner</param>
        /// <param name="teamID">The ID of the team to which this project belongs</param>
        /// <returns>True if creating the project succeeds, false otherwise</returns>
        public bool CreateProject(string name, DateTime startDate, Nullable<DateTime> endDate, int ownerID, int teamID)
        {
            User ownerUser = GetUserByID(ownerID);
            Team projectTeam = GetTeamByID(teamID);

            if (ownerUser == null || projectTeam == null)
            {
                return false;
            }

            Project newProject = new Project()
            {
                Project_name = name,
                Start_date = startDate,
                End_date = endDate,
                Owner = ownerID,
                User = ownerUser,
                Team_id = teamID,
                Team = projectTeam,
                Sprint = new System.Data.Linq.EntitySet<Sprint>()
            };

            bool result = commitChanges();

            if (result)
            {
                // Add the backlog to the project
                Sprint backog = new Sprint()
                {
                    Start_date = startDate,
                    End_date = endDate,
                    Project = newProject,
                    Project_id = newProject.Project_id,
                    Sprint_name = "Backlog",
                    Story = new System.Data.Linq.EntitySet<Story>()
                };

                result &= commitChanges();
            }

            return result;
        }

        /// <summary>
        /// Creates a new sprint
        /// </summary>
        /// <param name="name">The name of the sprint</param>
        /// <param name="startDate">The start date of the sprint</param>
        /// <param name="endDate">The end date of the sprint if it exists</param>
        /// <param name="projectID">The ID of the project to which to add the sprint</param>
        /// <returns>True if creating the sprint succeeds, false otherwise</returns>
        public bool CreateSprint(string name, DateTime startDate, Nullable<DateTime> endDate, int projectID)
        {
            Project curr = GetProjectByID(projectID);

            if (curr == null)
            {
                return false;
            }

            Sprint newSprint = new Sprint()
            {
                Sprint_name = name,
                Start_date = startDate,
                End_date = endDate,
                Project_id = projectID,
                Project = curr,
                Story = new System.Data.Linq.EntitySet<Story>()
            };

            return commitChanges();
        }

        /// <summary>
        /// Creates a new user story
        /// </summary>
        /// <param name="priority">The priority of the story</param>
        /// <param name="text">The text of the story</param>
        /// <param name="sprintID">The ID of the sprint to which to add the story</param>
        /// <returns>True if creating the story succeeds, false otherwise</returns>
        public bool CreateStory(int priority, string text, int sprintID)
        {
            Sprint curr = GetSprintByID(sprintID);

            if (curr == null)
            {
                return false;
            }

            Story newStory = new Story()
            {
                Priority_num = priority,
                Sprint_id = sprintID,
                Sprint = curr,
                Text = text,
                Task = new System.Data.Linq.EntitySet<Task>()
            };

            return commitChanges();
        }

        /// <summary>
        /// Creates a new task
        /// </summary>
        /// <param name="text">The text of the task</param>
        /// <param name="size">The size complexity of the task</param>
        /// <param name="value">The business value of the task</param>
        /// <param name="ownerID">The ID of the owner if the task if it exists</param>
        /// <param name="type">The type of this task</param>
        /// <param name="state">The state of this task</param>
        /// <param name="storyID">The ID of the story to which to add this task</param>
        /// <returns>True if creating the task succeeds, false otherwise</returns>
        public bool CreateTask(string text, int size, int value, int? ownerID, Binary type, Binary state, int storyID, Nullable<DateTime> completionDate)
        {
            Story curr = GetStoryByID(storyID);
            User ownerUser = null;

            if (ownerID.HasValue)
            {
                ownerUser = GetUserByID(ownerID.Value);
            }

            if (curr == null)
            {
                return false;
            }

            Task newTask = new Task()
            {
                Text = text,
                Business_value = value,
                Size_complexity = size,
                Completion_date = completionDate,
                Owner = ownerID,
                State = state,
                Type = type,
                Story = curr,
                Story_id = storyID,
                User = ownerUser
            };

            return commitChanges();
        }
        #endregion

        #region Change Existing Entities
        /// <summary>
        /// Moves a user to the given team
        /// </summary>
        /// <param name="userID">The ID of the user to move</param>
        /// <param name="teamID">The ID of the team to which to move the user</param>
        /// <returns>True if moving the user succeeds, false otherwise</returns>
        public bool MoveUserToTeam(int userID, int teamID)
        {
            User u = GetUserByID(userID);
            Team newTeam = GetTeamByID(teamID);
            Team oldTeam = GetTeamByID(u.Team_id);

            if (u == null || newTeam == null || oldTeam == null)
            {
                return false;
            }

            oldTeam.Team_.Remove(u);
            oldTeam.Team_.Add(u);

            u.Team_ = newTeam;
            u.Team_id = newTeam.Team_id;

            return commitChanges();
        }

        /// <summary>
        /// Changes an existing project
        /// </summary>
        /// <param name="projectID">The ID of the project to change</param>
        /// <param name="name">The name of the project</param>
        /// <param name="startDate">The start date of the project</param>
        /// <param name="endDate">The end date of the project if it exists</param>
        /// <param name="ownerID">The ID of the owner of the project</param>
        /// <param name="teamID">The ID of the team to which the project belongs</param>
        /// <returns>True if the changes succeed, false otherwise</returns>
        public bool ChangeProject(int projectID, string name, DateTime startDate, Nullable<DateTime> endDate, int ownerID, int teamID)
        {
            User ownerUser = GetUserByID(ownerID);
            Team projectTeam = GetTeamByID(teamID);
            Project project = GetProjectByID(projectID);

            if (project == null || projectTeam == null || ownerUser == null)
            {
                return false;
            }

            project.Project_name = name;
            project.Start_date = startDate;
            project.End_date = endDate;
            project.Owner = ownerID;
            project.User = ownerUser;
            project.Team_id = teamID;
            project.Team = projectTeam;

            return commitChanges();
        }

        /// <summary>
        /// Changes an existing sprint
        /// </summary>
        /// <param name="sprintID">The ID of the sprint to change</param>
        /// <param name="name">The name of the sprint</param>
        /// <param name="startDate">The start date of the sprint</param>
        /// <param name="endDate">The end date of the sprint if it exists</param>
        /// <returns>True if the changes succeed, false otherwise</returns>
        public bool ChangeSprint(int sprintID, string name, DateTime startDate, Nullable<DateTime> endDate)
        {
            Sprint sprint = GetSprintByID(sprintID);

            if (sprint == null)
            {
                return false;
            }

            sprint.Sprint_name = name;
            sprint.Start_date = startDate;
            sprint.End_date = endDate;

            return commitChanges();
        }

        /// <summary>
        /// Changes an existing story
        /// </summary>
        /// <param name="storyID">The ID of the story to change</param>
        /// <param name="priority">The priority of the story</param>
        /// <param name="text">The text of the story</param>
        /// <param name="sprintID">The ID of the sprint to which to move the story</param>
        /// <returns>True if the changes succeed, false otherwise</returns>
        public bool ChangeStory(int storyID, int priority, string text, int sprintID)
        {
            Story story = GetStoryByID(storyID);

            if (story == null)
            {
                return false;
            }

            if (sprintID != story.Sprint_id) // Move the story to a new sprint
            {
                Sprint oldSprint = GetSprintByID(story.Sprint_id);
                Sprint newSprint = GetSprintByID(sprintID);

                if (newSprint == null || oldSprint == null)
                {
                    return false;
                }

                newSprint.Story.Add(story);
                oldSprint.Story.Remove(story);

                story.Sprint_id = sprintID;
                story.Sprint = newSprint;
            }

            story.Priority_num = priority;
            story.Text = text;

            return commitChanges();
        }

        /// <summary>
        /// Changes an existing task
        /// </summary>
        /// <param name="taskID">The ID of the task to change</param>
        /// <param name="text">The text of the task</param>
        /// <param name="size">The size complexity of the task</param>
        /// <param name="value">The business value of the task</param>
        /// <param name="ownerID">The ID of the owner if it exists</param>
        /// <param name="type">The type of the task</param>
        /// <param name="state">The state of the task</param>
        /// <param name="completion">The date the task was completed if it exists</param>
        /// <returns>True if the changes succeed, false otherwise</returns>
        public bool ChangeTask(int taskID, string text, int size, int value, int? ownerID, Binary type, Binary state, Nullable<DateTime> completion)
        {
            Task task = GetTaskByID(taskID);
            User ownerUser = null;

            if (task == null)
            {
                return false;
            }

            if (ownerID.HasValue)
            {
                ownerUser = GetUserByID(ownerID.Value);

                if (ownerUser == null)
                {
                    return false;
                }
            }

            task.Text = text;
            task.Business_value = value;
            task.Size_complexity = size;
            task.Owner = ownerID;
            task.State = state;
            task.Type = type;
            task.User = ownerUser;
            task.Completion_date = completion;

            return commitChanges();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Commits changes made in the object model to the database
        /// </summary>
        /// <returns></returns>
        private bool commitChanges()
        {
            try
            {
                dbConnection.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
            catch (SqlException)
            {
                return false;
            }

            if (dbConnection.ChangeConflicts.Count == 0)
            {
                return true;
            }
            else
            {
                // Resolve each conflict by keeping the values currently in the db
                foreach (ObjectChangeConflict conflict in dbConnection.ChangeConflicts)
                {
                    conflict.Resolve(RefreshMode.OverwriteCurrentValues, true);
                }

                return false;
            }
        }
        #endregion
    }
}
