using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using DatabaseLayer;

namespace UnitTests
{
    /// <summary>
    /// Mock data model for unit testing
    /// </summary>
    class MockDataModel : IDataModel
    {
        private IEnumerable<User> _users;
        private IEnumerable<Team> _teams;
        private IEnumerable<Project> _projects;
        private IEnumerable<Sprint> _sprints;
        private IEnumerable<Story> _stories;
        private IEnumerable<Task> _tasks;

        public MockDataModel()
        {
            Binary one = new Binary(new byte[] { 0, 0, 1 });
            Binary four = new Binary(new byte[] { 0, 0, 4 });
            _users = new List<User>()
            {
                new User()
                {
                    Name = "Test User 1",
                    Password = "6Pl/upEE0epQR5SObftn+s2fW3M=",
                    Team_id = 1,
                    User_id = 1,
                    Role = four,
                },
                new User()
                {
                    Name = "Test User 2",
                    Password = "6Pl/upEE0epQR5SObftn+s2fW3M=",
                    Team_id = 2,
                    User_id = 2,
                    Role = four,
                }
            };

            _teams = new List<Team>()
            {
                new Team()
                {
                    Team_name = "Team 1",
                    Team_id = 1,
                    Manager = 1,
                    Team_lead = 1,
                },
                new Team()
                {
                    Team_name = "Team 2",
                    Team_id = 2,
                    Manager = 2,
                    Team_lead = 2,
                }
            };
            _users.ElementAt(0).Team_ = _teams.ElementAt(0);
            _users.ElementAt(1).Team_ = _teams.ElementAt(1);

            _projects = new List<Project>()
            {
                new Project()
                {
                    Project_id = 1,
                    Owner = 1,
                    Team_id = 1,
                    Project_name = "Project 1",
                    Start_date = new DateTime(2011, 10, 29),
                    End_date = null
                },
                new Project()
                {
                    Project_id = 2,
                    Owner = 2,
                    Team_id = 2,
                    Project_name = "Project 2",
                    Start_date = new DateTime(2012, 10, 29),
                    End_date = new DateTime(2012, 12, 20),
                }
            };

            _sprints = new List<Sprint>()
            {
                new Sprint()
                {
                    Sprint_id = 1,
                    Project_id = 1,
                    Sprint_name = "Backlog",
                    Start_date = new DateTime(2011, 10, 29),
                    End_date = new DateTime(2011, 12, 20),
                },
                new Sprint()
                {
                    Sprint_id = 2,
                    Project_id = 2,
                    Sprint_name = "Backlog",
                    Start_date = new DateTime(2012, 10, 29),
                    End_date = null
                }
            };

            _stories = new List<Story>()
            {
                new Story()
                {
                    Story_id = 1,
                    Sprint_id = 1,
                    Priority_num = 100,
                    Text = "User Story 1",
                },
                new Story()
                {
                    Story_id = 2,
                    Sprint_id = 2,
                    Priority_num = 30,
                    Text = "User Story 2",
                }
            };

            _tasks = new List<Task>()
            {
                new Task()
                {
                    Task_id = 1,
                    Story_id = 1,
                    Business_value = 1,
                    Size_complexity = 1,
                    Completion_date = null,
                    Owner = null,
                    Text = "Task 1",
                    State = one,
                    Type = one,
                },
                new Task()
                {
                    Task_id = 3,
                    Story_id = 1,
                    Business_value = 1,
                    Size_complexity = 1,
                    Completion_date = new DateTime(2011, 10, 31),
                    Owner = null,
                    Text = "Task 1",
                    State = one,
                    Type = one,
                },
                new Task()
                {
                    Task_id = 2,
                    Story_id = 2,
                    Business_value = 3,
                    Size_complexity = 8,
                    Completion_date = new DateTime(2012, 11, 1),
                    Owner = 2,
                    Text = "Task 2",
                    State = one,
                    Type = one,
                }
            };
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _projects;
        }

        public IEnumerable<Project> GetProjectsByTeam(int teamId)
        {
            IEnumerable<Project> result = from p in _projects
                                          where p.Team_id == teamId
                                          select p;

            return result;
        }

        public IEnumerable<Project> GetProjectsByOwner(int ownerID)
        {
            IEnumerable<Project> result = from p in _projects
                                          where p.Owner == ownerID
                                          select p;

            return result;
        }

        public Project GetProjectByID(int projectID)
        {
            IEnumerable<Project> result = from p in _projects
                                          where p.Project_id == projectID
                                          select p;

            return result.FirstOrDefault();
        }

        public IEnumerable<Sprint> GetSprintsForProject(int projectID)
        {
            IEnumerable<Sprint> result = from s in _sprints
                                         where s.Project_id == projectID
                                         select s;

            return result;
        }

        public Sprint GetProjectBacklog(int projectID)
        {
            IEnumerable<Sprint> result = from s in _sprints
                                         where s.Sprint_name == "Backlog"
                                         select s;

            return result.FirstOrDefault();
        }

        public Sprint GetSprintByID(int sprintID)
        {
            IEnumerable<Sprint> result = from s in _sprints
                                         where s.Sprint_id == sprintID
                                         select s;

            return result.FirstOrDefault();
        }

        public IEnumerable<Story> GetStoriesForSprint(int sprintID)
        {
            IEnumerable<Story> result = from s in _stories
                                        where s.Sprint_id == sprintID
                                        select s;

            return result;
        }

        public Story GetStoryByID(int storyID)
        {
            IEnumerable<Story> result = from s in _stories
                                        where s.Story_id == storyID
                                        select s;

            return result.FirstOrDefault();
        }

        public IEnumerable<Task> GetAllTasks()
        {
            return _tasks;
        }

        public IEnumerable<Task> GetTasksForStory(int storyID)
        {
            IEnumerable<Task> result = from t in _tasks
                                       where t.Story_id == storyID
                                       select t;

            return result;
        }

        public IEnumerable<Task> GetAllTasksForSprint(int sprintID)
        {
            IEnumerable<Task> result = from t in _tasks
                                       from s in _stories
                                       where t.Story_id == s.Story_id && s.Sprint_id == sprintID
                                       select t;

            return result;
        }

        public IEnumerable<Task> GetTasksForUser(int userID)
        {
            IEnumerable<Task> result = from t in _tasks
                                       where t.Owner.HasValue && t.Owner.Value == userID
                                       select t;

            return result;
        }

        public Task GetTaskByID(int taskID)
        {
            IEnumerable<Task> result = from t in _tasks
                                       where t.Task_id == taskID
                                       select t;

            return result.FirstOrDefault();
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _teams;
        }

        public Team GetTeamByID(int teamID)
        {
            IEnumerable<Team> result = from t in _teams
                                       where t.Team_id == teamID
                                       select t;

            return result.FirstOrDefault();
        }

        public User GetUserByID(int userID)
        {
            IEnumerable<User> result = from u in _users
                                       where u.User_id == userID
                                       select u;

            return result.FirstOrDefault();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public IEnumerable<User> GetTeamMembers(int teamID)
        {
            IEnumerable<User> result = from u in _users
                                       where u.Team_id == teamID
                                       select u;

            return result;
        }

        public IEnumerable<User> GetUsersNotInTeam(int teamID)
        {
            IEnumerable<User> result = from u in _users
                                       where u.Team_id != teamID
                                       select u;

            return result;
        }

        public User AuthenticateUser(int userID, string password)
        {
            return _users.Where(u => u.User_id == userID && u.Password == password).FirstOrDefault();
        }

        public bool CreateTeam(string name, int managerID, int leadID)
        {
            return (name != "Invalid Team");
        }

        public bool CreateProject(string name, DateTime startDate, DateTime? endDate, int ownerID, int teamID)
        {
            return (name != "Invalid Project");
        }

        public bool CreateSprint(string name, DateTime startDate, DateTime? endDate, int projectID)
        {
            return (name != "Invalid Sprint");
        }

        public bool CreateStory(int priority, string text, int sprintID)
        {
            return (text != "Fail creating");
        }

        public bool CreateTask(string text, int size, int value, int? ownerID, System.Data.Linq.Binary type, System.Data.Linq.Binary state, int storyID)
        {
            return (text != "Fail creating");
        }

        public bool MoveUserToTeam(int userID, int teamID)
        {
            return (teamID > 0);
        }

        public bool ChangeProject(int projectID, string name, DateTime startDate, DateTime? endDate, int ownerID, int teamID)
        {
            return (teamID > 0);
        }

        public bool ChangeSprint(int sprintID, string name, DateTime startDate, DateTime? endDate)
        {
            return (name != "Invalid Sprint");
        }

        public bool ChangeStory(int storyID, int priority, string text, int sprintID)
        {
            return (text != "Fail changing");
        }

        public bool ChangeTask(int taskID, string text, int size, int value, int? ownerID, System.Data.Linq.Binary type, System.Data.Linq.Binary state, DateTime? completion)
        {
            return (text != "Fail changing");
        }
    }
}
