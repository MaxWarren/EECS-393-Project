using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLayer;

namespace UnitTests
{
    /// <summary>
    /// Mock data model for unit testing
    /// </summary>
    class MockDataModel : IDataModel
    {
        public IEnumerable<Project> GetAllProjects()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetProjectsByTeam(int teamId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetProjectsByOwner(int ownerID)
        {
            throw new NotImplementedException();
        }

        public Project GetProjectByID(int projectID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sprint> GetSprintsForProject(int projectID)
        {
            throw new NotImplementedException();
        }

        public Sprint GetProjectBacklog(int projectID)
        {
            throw new NotImplementedException();
        }

        public Sprint GetSprintByID(int sprintID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Story> GetStoriesForSprint(int sprintID)
        {
            throw new NotImplementedException();
        }

        public Story GetStoryByID(int storyID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task> GetAllTasks()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task> GetTasksForStory(int storyID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task> GetAllTasksForSprint(int sprintID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task> GetTasksForUser(int userID)
        {
            throw new NotImplementedException();
        }

        public Task GetTaskByID(int taskID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Team> GetAllTeams()
        {
            throw new NotImplementedException();
        }

        public Team GetTeamByID(int teamID)
        {
            throw new NotImplementedException();
        }

        public User GetUserByID(int userID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetTeamMembers(int teamID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsersNotInTeam(int teamID)
        {
            throw new NotImplementedException();
        }

        public User AuthenticateUser(int userID, string password)
        {
            throw new NotImplementedException();
        }

        public bool CreateTeam(string name, int managerID, int leadID)
        {
            throw new NotImplementedException();
        }

        public bool CreateProject(string name, DateTime startDate, DateTime? endDate, int ownerID, int teamID)
        {
            throw new NotImplementedException();
        }

        public bool CreateSprint(string name, DateTime startDate, DateTime? endDate, int projectID)
        {
            throw new NotImplementedException();
        }

        public bool CreateStory(int priority, string text, int sprintID)
        {
            throw new NotImplementedException();
        }

        public bool CreateTask(string text, int size, int value, int? ownerID, System.Data.Linq.Binary type, System.Data.Linq.Binary state, int storyID)
        {
            throw new NotImplementedException();
        }

        public bool MoveUserToTeam(int userID, int teamID)
        {
            throw new NotImplementedException();
        }

        public bool ChangeProject(int projectID, string name, DateTime startDate, DateTime? endDate, int ownerID, int teamID)
        {
            throw new NotImplementedException();
        }

        public bool ChangeSprint(int sprintID, string name, DateTime startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public bool ChangeStory(int storyID, int priority, string text, int sprintID)
        {
            throw new NotImplementedException();
        }

        public bool ChangeTask(int taskID, string text, int size, int value, int? ownerID, System.Data.Linq.Binary type, System.Data.Linq.Binary state, DateTime? completion)
        {
            throw new NotImplementedException();
        }
    }
}
