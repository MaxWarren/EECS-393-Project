using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace DatabaseLayer
{
    /// <summary>
    /// Static class that interfaces with the database
    /// </summary>
    public static class DataModel
    {
        /// <summary>
        /// Connection to the database
        /// </summary>
        private static Eecs393_project dbConnection;

        /// <summary>
        /// Connection string for connecting to the database
        /// </summary>
        private static string connString;

        /// <summary>
        /// Initialize the database connection
        /// </summary>
        static DataModel()
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

        /// <summary>
        /// Get all projects in the database
        /// </summary>
        /// <returns>A list of all projects in the database</returns>
        public static IEnumerable<Project> GetAllProjects()
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
                return null; // TODO add error handling for db failure
            }
        }

        /// <summary>
        /// Gets all projects belonging to a specific team
        /// </summary>
        /// <param name="teamId">The ID of the team for which to search</param>
        /// <returns>A list of all projects belonging to the given team</returns>
        public static IEnumerable<Project> GetProjectsByTeam(int teamId)
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
                return null; // TODO add error handling for db failure
            }
        }

        /// <summary>
        /// Gets all projects owned by a specific user
        /// </summary>
        /// <param name="ownerID">The ID of the user for whom to search</param>
        /// <returns>A list of all projects owned by the given user</returns>
        public static IEnumerable<Project> GetProjectsByOwner(int ownerID)
        {
            try
            {
                IEnumerable<Project> projects = from p in dbConnection.Project
                                                where p.Owner == ownerID
                                                orderby p.Project_name ascending
                                                select p;
                return projects;
            }
            catch (Exception)
            {
                return null; // TODO add error handling for db failure
            }
        }

        /// <summary>
        /// Gets the project with the given ID
        /// </summary>
        /// <param name="projectID">The project ID for which to search</param>
        /// <returns>The project with the given ID</returns>
        public static Project GetProjectByID(int projectID)
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
                return null; // TODO add error handling for db failure
            }
        }

        /// <summary>
        /// Gets all sprints for a project
        /// </summary>
        /// <param name="projectID">The project for which to retrieve sprints</param>
        /// <returns>A list of all sprints for the given project</returns>
        public static IEnumerable<Sprint> GetSprintsForProject(int projectID)
        {
            try
            {
                IEnumerable<Sprint> sprints = from s in dbConnection.Sprint
                                              where s.Project_id == projectID
                                              where !s.Sprint_name.Equals("Backlog") // Do not include the backlog
                                              orderby s.Start_date descending
                                              select s;

                return sprints;
            }
            catch (Exception)
            {
                return null; // TODO add error handling for db failure
            }
        }

        /// <summary>
        /// Gets the backlog for a project
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public static Sprint GetProjectBacklog(int projectID)
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
                return null; // TODO add error handling for db failure
            }
        }

        /// <summary>
        /// Gets the sprint with the given ID
        /// </summary>
        /// <param name="sprintID">The sprint ID for which to search</param>
        /// <returns>The sprint with the given ID</returns>
        public static Sprint GetSprintByID(int sprintID)
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
                return null; // TODO add error handling for db failure
            }
        }

        /// <summary>
        /// Gets all user stories assigned to a sprint
        /// </summary>
        /// <param name="sprintID">The ID of the sprint for which to search</param>
        /// <returns>A list of all user stories assigned to the given sprint</returns>
        public static IEnumerable<Story> GetStoriesForSprint(int sprintID)
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
                return null; // TODO add error handling for db failure
            }
        }

        /// <summary>
        /// Gets a user story with the given ID
        /// </summary>
        /// <param name="storyID">The ID of the story for which to search</param>
        /// <returns>The story with the given ID</returns>
        public static Story GetStoryByID(int storyID)
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
                return null; // TODO add error handling for db failure
            }
        }

        /// <summary>
        /// Gets all tasks for a given user story
        /// </summary>
        /// <param name="storyID">The ID of the story for which to search</param>
        /// <returns>A list of all tasks for the given user story</returns>
        public static IEnumerable<Task> GetTasksForStory(int storyID)
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
                return null; // TODO add error handling for db failure
            }
        }

        /// <summary>
        /// Gets all tasks owned by a given user
        /// </summary>
        /// <param name="userID">The ID of the user for which to search</param>
        /// <returns>A list of all tasks belonging to the given user</returns>
        public static IEnumerable<Task> GetTasksForUser(int userID)
        {
            try
            {
                IEnumerable<Task> tasks = from t in dbConnection.Task
                                          where t.Owner == userID
                                          orderby t.Business_value descending
                                          select t;

                return tasks;
            }
            catch (Exception)
            {
                return null; // TODO add error handling for db failure
            }
        }

        /// <summary>
        /// Gets a task with the given ID
        /// </summary>
        /// <param name="taskID">The ID of the task for which to search</param>
        /// <returns>The task with the given ID</returns>
        public static Task GetTaskByID(int taskID)
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
                return null; // TODO add error handling for db failure
            }
        }

        /// <summary>
        /// Gets all teams in the database
        /// </summary>
        /// <returns>A list of all teams in the database</returns>
        public static IEnumerable<Team> GetAllTeams()
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
                return null; // TODO add error handling for db failure
            }
        }

        /// <summary>
        /// Gets the team with the given ID
        /// </summary>
        /// <param name="teamID">The ID of the team for which to search</param>
        /// <returns>The team with the given ID</returns>
        public static Team GetTeamByID(int teamID)
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
                return null; // TODO add error handling for db failure
            }
        }

        /// <summary>
        /// Gets all members of a team
        /// </summary>
        /// <param name="teamID">The ID of the team for which to search</param>
        /// <returns>A list of all members of the given team</returns>
        public static IEnumerable<User> GetTeamMembers(int teamID)
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
                return null; // TODO add error handling for db failure
            }
        }

        /// <summary>
        /// Gets all Users not in a team
        /// </summary>
        /// <param name="teamID">The ID of the team for which to search</param>
        /// <returns>A list of all Users not in the given team</returns>
        public static IEnumerable<User> GetUsersNotInTeam(int teamID)
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
                return null; // TODO add error handling for db failure
            }
        }

        /// <summary>
        /// Authenticates a user
        /// </summary>
        /// <param name="userID">The ID of the user to authenticate</param>
        /// <param name="password">The password provided by the user</param>
        /// <returns>The User if authentication succeeded, false otherwise</returns>
        public static User AuthenticateUser(int userID, string password)
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
                return null; // TODO add error handling for db failure
            }
        }

        public static bool CommitChanges()
        {
            dbConnection.SubmitChanges(ConflictMode.ContinueOnConflict);

            if (dbConnection.ChangeConflicts.Count == 0)
            {
                return true;
            }
            else
            {
                // TODO add error handling
                return false;
            }
        }
    }
}
