using System;
using System.Collections.Generic;
using System.Data.Linq;

namespace DatabaseLayer
{
    public interface IDataModel
    {
        #region Get Projects
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
        #endregion

        #region Get Sprints
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
        #endregion

        #region Get Stories
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
        #endregion

        #region Get Tasks
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
        #endregion

        #region Get Teams
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
        #endregion

        #region Get Users
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
        #endregion

        #region Create New Entities
        /// <summary>
        /// Creates a new team
        /// </summary>
        /// <param name="name">The name of the team</param>
        /// <param name="managerID">The ID of the team's manager</param>
        /// <param name="leadID">The ID of the team lead</param>
        /// <returns>True if creating the team succeeds, false otherwise</returns>
        bool CreateTeam(string name, int managerID, int leadID);

        /// <summary>
        /// Creates a new project
        /// </summary>
        /// <param name="name">The name of the project</param>
        /// <param name="startDate">The start date of the project</param>
        /// <param name="endDate">The end date of the project if it exists</param>
        /// <param name="ownerID">The ID of the project owner</param>
        /// <param name="teamID">The ID of the team to which this project belongs</param>
        /// <returns>True if creating the project succeeds, false otherwise</returns>
        bool CreateProject(string name, DateTime startDate, Nullable<DateTime> endDate, int ownerID, int teamID);

        /// <summary>
        /// Creates a new sprint
        /// </summary>
        /// <param name="name">The name of the sprint</param>
        /// <param name="startDate">The start date of the sprint</param>
        /// <param name="endDate">The end date of the sprint if it exists</param>
        /// <param name="projectID">The ID of the project to which to add the sprint</param>
        /// <returns>True if creating the sprint succeeds, false otherwise</returns>
        bool CreateSprint(string name, DateTime startDate, Nullable<DateTime> endDate, int projectID);

        /// <summary>
        /// Creates a new user story
        /// </summary>
        /// <param name="priority">The priority of the story</param>
        /// <param name="text">The text of the story</param>
        /// <param name="sprintID">The ID of the sprint to which to add the story</param>
        /// <returns>True if creating the story succeeds, false otherwise</returns>
        bool CreateStory(int priority, string text, int sprintID);

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
        bool CreateTask(string text, int size, int value, int? ownerID, Binary type, Binary state, int storyID);
        #endregion

        #region Change Existing Entities
        /// <summary>
        /// Moves a user to the given team
        /// </summary>
        /// <param name="userID">The ID of the user to move</param>
        /// <param name="teamID">The ID of the team to which to move the user</param>
        /// <returns>True if moving the user succeeds, false otherwise</returns>
        bool MoveUserToTeam(int userID, int teamID);

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
        bool ChangeProject(int projectID, string name, DateTime startDate, Nullable<DateTime> endDate, int ownerID, int teamID);

        /// <summary>
        /// Changes an existing sprint
        /// </summary>
        /// <param name="sprintID">The ID of the sprint to change</param>
        /// <param name="name">The name of the sprint</param>
        /// <param name="startDate">The start date of the sprint</param>
        /// <param name="endDate">The end date of the sprint if it exists</param>
        /// <returns>True if the changes succeed, false otherwise</returns>
        bool ChangeSprint(int sprintID, string name, DateTime startDate, Nullable<DateTime> endDate);

        /// <summary>
        /// Changes an existing story
        /// </summary>
        /// <param name="storyID">The ID of the story to change</param>
        /// <param name="priority">The priority of the story</param>
        /// <param name="text">The text of the story</param>
        /// <param name="sprintID">The ID of the sprint to which to move the story</param>
        /// <returns>True if the changes succeed, false otherwise</returns>
        bool ChangeStory(int storyID, int priority, string text, int sprintID);

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
        bool ChangeTask(int taskID, string text, int size, int value, int? ownerID, Binary type, Binary state, Nullable<DateTime> completion);
        #endregion
    }
}
