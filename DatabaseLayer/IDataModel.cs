using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLayer
{
    public interface IDataModel
    {
        /// <summary>
        /// Get all projects in the database
        /// </summary>
        /// <returns>A list of all projects in the database</returns>
        IEnumerable<Project> GetAllProjects();

        /// <summary>
        /// Gets all projects belonging to a specific team
        /// </summary>
        /// <param name="teamId">The ID of the team for which to search</param>
        /// <returns>A list of all projects belonging to the given team</returns>
        IEnumerable<Project> GetProjectsByTeam(int teamId);

        /// <summary>
        /// Gets all projects owned by a specific user
        /// </summary>
        /// <param name="ownerID">The ID of the user for whom to search</param>
        /// <returns>A list of all projects owned by the given user</returns>
        IEnumerable<Project> GetProjectsByOwner(int ownerID);

        /// <summary>
        /// Gets the project with the given ID
        /// </summary>
        /// <param name="projectID">The project ID for which to search</param>
        /// <returns>The project with the given ID</returns>
        Project GetProjectByID(int projectID);

        /// <summary>
        /// Gets all sprints for a project
        /// </summary>
        /// <param name="projectID">The project for which to retrieve sprints</param>
        /// <returns>A list of all sprints for the given project</returns>
        IEnumerable<Sprint> GetSprintsForProject(int projectID);

        /// <summary>
        /// Gets the backlog for a project
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        Sprint GetProjectBacklog(int projectID);

        /// <summary>
        /// Gets the sprint with the given ID
        /// </summary>
        /// <param name="sprintID">The sprint ID for which to search</param>
        /// <returns>The sprint with the given ID</returns>
        Sprint GetSprintByID(int sprintID);

        /// <summary>
        /// Gets all user stories assigned to a sprint
        /// </summary>
        /// <param name="sprintID">The ID of the sprint for which to search</param>
        /// <returns>A list of all user stories assigned to the given sprint</returns>
        IEnumerable<Story> GetStoriesForSprint(int sprintID);

        /// <summary>
        /// Gets a user story with the given ID
        /// </summary>
        /// <param name="storyID">The ID of the story for which to search</param>
        /// <returns>The story with the given ID</returns>
        Story GetStoryByID(int storyID);

        /// <summary>
        /// Gets all tasks for a given user story
        /// </summary>
        /// <param name="storyID">The ID of the story for which to search</param>
        /// <returns>A list of all tasks for the given user story</returns>
        IEnumerable<Task> GetTasksForStory(int storyID);

        /// <summary>
        /// Gets all tasks owned by a given user
        /// </summary>
        /// <param name="userID">The ID of the user for which to search</param>
        /// <returns>A list of all tasks belonging to the given user</returns>
        IEnumerable<Task> GetTasksForUser(int userID);

        /// <summary>
        /// Gets a task with the given ID
        /// </summary>
        /// <param name="taskID">The ID of the task for which to search</param>
        /// <returns>The task with the given ID</returns>
        Task GetTaskByID(int taskID);

        /// <summary>
        /// Gets all teams in the database
        /// </summary>
        /// <returns>A list of all teams in the database</returns>
        IEnumerable<Team> GetAllTeams();

        /// <summary>
        /// Gets the team with the given ID
        /// </summary>
        /// <param name="teamID">The ID of the team for which to search</param>
        /// <returns>The team with the given ID</returns>
        Team GetTeamByID(int teamID);

        /// <summary>
        /// Gets the user with the given ID
        /// </summary>
        /// <param name="userID">The ID of the user for which to search</param>
        /// <returns>The user with the given ID</returns>
        User GetUserByID(int userID);

        /// <summary>
        /// Gets all users in the database
        /// </summary>
        /// <returns>A list of all users in the database</returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Gets all members of a team
        /// </summary>
        /// <param name="teamID">The ID of the team for which to search</param>
        /// <returns>A list of all members of the given team</returns>
        IEnumerable<User> GetTeamMembers(int teamID);

        /// <summary>
        /// Gets all Users not in a team
        /// </summary>
        /// <param name="teamID">The ID of the team for which to search</param>
        /// <returns>A list of all Users not in the given team</returns>
        IEnumerable<User> GetUsersNotInTeam(int teamID);

        /// <summary>
        /// Authenticates a user
        /// </summary>
        /// <param name="userID">The ID of the user to authenticate</param>
        /// <param name="password">The password provided by the user</param>
        /// <returns>The User if authentication succeeded, false otherwise</returns>
        User AuthenticateUser(int userID, string password);

        /// <summary>
        /// Commits changes made in the object model to the database
        /// </summary>
        /// <returns></returns>
        bool CommitChanges();
    }
}
