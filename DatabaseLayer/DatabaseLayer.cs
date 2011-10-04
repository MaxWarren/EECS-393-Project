using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseLayer
{
    /// <summary>
    /// Static class that interfaces with the database
    /// </summary>
    public static class DatabaseLayer
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
        static DatabaseLayer()
        {
            // Set connection string
            connString = string.Format("user id={0};password={1};server={2};Trusted_Connection=yes;database={3};connection timeout={4}",
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
                                                orderby p.ProjectName ascending
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
                                                where p.TeamID == teamId
                                                orderby p.ProjectName ascending
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
                                                where p.OwnerID == ownerID
                                                orderby p.ProjectName ascending
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
                                                where p.ProjectID == projectID
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
                                              where s.ProjectID == projectID
                                              where !s.SprintName.Equals("Backlog") // Do not include the backlog
                                              orderby s.StartDate descending
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
                                              where s.ProjectID == projectID
                                              where s.SprintName.Equals("Backlog")
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
                                              where s.SprintID == sprintID
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
                                             where s.SprintID == sprintID
                                             orderby s.Priority ascending
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
                                             where s.StoryID == storyID
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
                                          where t.StoryID == storyID
                                          orderby t.BusinessValue descending
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
                                          where t.OwnerID == userID
                                          orderby t.BusinessValue descending
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
                                          where t.TaskID == taskID
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
                                          orderby t.TeamName ascending
                                          select t;

                return teams;
            }
            catch (Exception)
            {
                return null; // TODO add error handling for db failure
            }
        }
    }
}
