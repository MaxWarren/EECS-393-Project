using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DatabaseLayer;

namespace ViewModel
{
    /// <summary>
    /// SPMS View Model
    /// </summary>
    public class SPMSViewModel
    {
        /// <summary>
        /// Indicates whether or not a user is logged in
        /// </summary>
        private bool _isLoggedIn;

        private ObservableCollection<ProjectView> _projectsForUser;
        private ObservableCollection<TaskView> _tasksForUser;
        private ObservableCollection<SprintView> _sprintsForProject;
        private ObservableCollection<StoryView> _storiesForSprint;
        private ObservableCollection<TaskView> _tasksForStory;
        private ObservableCollection<TeamView> _allTeams;

        /// <summary>
        /// Indicates if the current user is a manager
        /// </summary>
        public bool IsManager { get; set; }

        /// <summary>
        /// The currently logged in user
        /// </summary>
        public UserView CurrUser { get; set; }

        /// <summary>
        /// The team to which the current user belongs
        /// </summary>
        public TeamView CurrTeam { get; set; }

        /// <summary>
        /// The project most recently selected by the user
        /// </summary>
        public ProjectView CurrProject { get; set; }

        /// <summary>
        /// The sprint most recently selected by the user
        /// </summary>
        public SprintView CurrSprint { get; set; }

        /// <summary>
        /// The user story most recently selected by the user
        /// </summary>
        public StoryView CurrStory { get; set; }

        /// <summary>
        /// The task most recently selected by the user
        /// </summary>
        public TaskView CurrTask { get; set; }

        /// <summary>
        /// A list of all projects that belong to the team to which the current user belongs
        /// </summary>
        public ObservableCollection<ProjectView> ProjectsForUser
        {
            get { return _projectsForUser; }
            private set { _projectsForUser = value; }
        }

        /// <summary>
        /// A list of all tasks assigned to the current user
        /// </summary>
        public ObservableCollection<TaskView> TasksForUser
        {
            get { return _tasksForUser; }
            private set { _tasksForUser = value; }
        }

        /// <summary>
        /// A list of all sprints that make up the current project
        /// </summary>
        public ObservableCollection<SprintView> SprintsForProject
        {
            get { return _sprintsForProject; }
            private set { _sprintsForProject = value; }
        }

        /// <summary>
        /// A list of all user stories belonging to the current sprint
        /// </summary>
        public ObservableCollection<StoryView> StoriesForSprint
        {
            get { return _storiesForSprint; }
            private set { _storiesForSprint = value; }
        }

        /// <summary>
        /// A list of all tasks belonging to the current user story
        /// </summary>
        public ObservableCollection<TaskView> TasksForStory
        {
            get { return _tasksForStory; }
            private set { _tasksForStory = value; }
        }

        /// <summary>
        /// A list of all teams in the database
        /// </summary>
        public ObservableCollection<TeamView> AllTeams
        {
            get { return _allTeams; }
            private set { _allTeams = value; }
        }

        /// <summary>
        /// Initializes the view model
        /// </summary>
        public SPMSViewModel()
        {
            _isLoggedIn = false;

            // Set all the observable collections to empty lists
            _projectsForUser = new ObservableCollection<ProjectView>();
            _sprintsForProject = new ObservableCollection<SprintView>();
            _storiesForSprint = new ObservableCollection<StoryView>();
            _tasksForStory = new ObservableCollection<TaskView>();
            _tasksForUser = new ObservableCollection<TaskView>();
            _allTeams = new ObservableCollection<TeamView>();
        }

        /// <summary>
        /// Authenticates the user
        /// </summary>
        /// <param name="userId">The ID of the user to authenticate</param>
        /// <param name="password">The user's password</param>
        /// <returns>True if authentication succeeds, false otherwise</returns>
        public bool AuthenticateUser(int userId, string password)
        {
            string passHash = hashPassword(password);

            User curr = DataModel.AuthenticateUser(userId, passHash);

            if (curr == null) //  Authentication failed
            {
                return false;
            }

            CurrUser = new UserView(curr); // Store the user
            CurrTeam = new TeamView(curr.Team_); // Store the team

            _isLoggedIn = true;
            IsManager = !(CurrUser.Role == UserRole.Manager);

            UpdateProjectsForUser();
            UpdateTasksForUser();

            return true;
        }

        /// <summary>
        /// Gets a list of users in a team and a list of users not in a team
        /// </summary>
        /// <param name="team">The team for which to search</param>
        /// <returns>A tuple, the first element of which is the list of team members and the second is the list of Users not in the team</returns>
        public Tuple<ObservableCollection<UserView>, ObservableCollection<UserView>> GetTeamMembers(TeamView team)
        {
            if (team == null) // Bad value
            {
                throw new ArgumentNullException("Arguments to GetTeamMembers must not be null");
            }

            var result = new Tuple<ObservableCollection<UserView>, ObservableCollection<UserView>>(
                new ObservableCollection<UserView>(),
                new ObservableCollection<UserView>());

            IEnumerable<User> members = DataModel.GetTeamMembers(team.TeamID);
            IEnumerable<User> nonMembers = DataModel.GetUsersNotInTeam(team.TeamID);

            if (members != null) // An error occurred
            {
                foreach (User u in members)
                {
                    result.Item1.Add(new UserView(u));
                }
            }

            if (nonMembers != null) // An error occurred
            {
                foreach (User u in nonMembers)
                {
                    result.Item2.Add(new UserView(u));
                }
            }

            return result;
        }

        /// <summary>
        /// Changes a user's team
        /// </summary>
        /// <param name="user">The user to change</param>
        /// <param name="team">The team to which to move the user</param>
        /// <returns>True if the move succeeds, false otherwise</returns>
        public bool ChangeTeam(UserView user, TeamView team)
        {
            if (user == null || team == null)
            {
                throw new ArgumentNullException("Arguments to ChangeTeam must not be null");
            }

            User u = DataModel.GetUserByID(user.UserId);
            Team t = DataModel.GetTeamByID(team.TeamID);

            u.Team_ = t;
            u.Team_id = t.Team_id;

            return DataModel.CommitChanges();
        }

        /// <summary>
        /// Updates the ProjectsForUser collection
        /// </summary>
        /// <returns>True if the update succeeds, false otherwise</returns>
        public bool UpdateProjectsForUser()
        {
            if (!_isLoggedIn) // No one is logged in
            {
                throw new InvalidOperationException("User must be logged in");
            }

            _projectsForUser.Clear(); // Clear the existing entries

            IEnumerable<Project> projects = DataModel.GetProjectsByTeam(CurrTeam.TeamID);
            if (projects == null) // An error occured
            {
                return false;
            }

            foreach (Project p in projects)
            {
                _projectsForUser.Add(new ProjectView(p));
            }

            return true;
        }

        /// <summary>
        /// Updates the SprintsForProject collection
        /// </summary>
        /// <returns>True if the update succeeds, false otherwise</returns>
        public bool UpdateSprintsForProject()
        {
            if (!_isLoggedIn || CurrProject == null) // No one is logged in or a project has not been selected
            {
                throw new InvalidOperationException("User must be logged in and CurrProject must be set");
            }

            _sprintsForProject.Clear(); // Clear the existing entries

            IEnumerable<Sprint> sprints = DataModel.GetSprintsForProject(CurrProject.ProjectID);
            if (sprints == null) // An error occured
            {
                return false;
            }

            foreach (Sprint s in sprints)
            {
                _sprintsForProject.Add(new SprintView(s));
            }

            return true;
        }

        /// <summary>
        /// Updates the StoriesForSprint collection
        /// </summary>
        /// <returns>True if the update succeeds, false otherwise</returns>
        public bool UpdateStoriesForSprint()
        {
            if (!_isLoggedIn || CurrSprint == null) // No one is logged in or a sprint has not been selected
            {
                throw new InvalidOperationException("User must be logged in and CurrSprint must be set");
            }

            _storiesForSprint.Clear(); // Clear the existing entries

            IEnumerable<Story> stories = DataModel.GetStoriesForSprint(CurrSprint.SprintID);
            if (stories == null) // An error occured
            {
                return false;
            }

            foreach (Story s in stories)
            {
                _storiesForSprint.Add(new StoryView(s));
            }

            return true;
        }

        /// <summary>
        /// Updates the TasksForStory collection
        /// </summary>
        /// <returns>True if the update succeeds, false otherwise</returns>
        public bool UpdateTasksForStory()
        {
            if (!_isLoggedIn || CurrStory == null) // No one is logged in or a user story has not been selected
            {
                throw new InvalidOperationException("User must be logged in and CurrStory must be set");
            }

            _tasksForStory.Clear(); // Clear the existing entries

            IEnumerable<Task> tasks = DataModel.GetTasksForStory(CurrStory.StoryID);
            if (tasks == null) // An error occured
            {
                return false;
            }

            foreach (Task t in tasks)
            {
                _tasksForStory.Add(new TaskView(t));
            }

            return true;
        }

        /// <summary>
        /// Updates the AllTeams collection
        /// </summary>
        /// <returns>True if the update succeeds, false otherwise</returns>
        public bool UpdateAllTeams()
        {
            if (!_isLoggedIn) // No one is logged in
            {
                throw new InvalidOperationException("User must be logged in");
            }

            _allTeams.Clear(); // Clear the existing entries

            IEnumerable<Team> teams = DataModel.GetAllTeams();
            if (teams == null) // An error occured
            {
                return false;
            }

            foreach (Team t in teams)
            {
                _allTeams.Add(new TeamView(t));
            }

            return true;
        }

        /// <summary>
        /// Updates the TasksForUser collection
        /// </summary>
        /// <returns>True if the update succeeds, false otherwise</returns>
        public bool UpdateTasksForUser()
        {
            if (!_isLoggedIn) // No one is logged in
            {
                throw new InvalidOperationException("User must be logged in");
            }

            _tasksForUser.Clear(); // Clear the existing entries

            IEnumerable<Task> tasks = DataModel.GetTasksForUser(CurrUser.UserId);
            if (tasks == null) // An error occured
            {
                return false;
            }

            foreach (Task t in tasks)
            {
                _tasksForUser.Add(new TaskView(t));
            }

            return true;
        }

        /// <summary>
        /// Get all managers in the database
        /// </summary>
        /// <returns>A list of all managers in the database</returns>
        public ObservableCollection<UserView> GetManagers()
        {
            IEnumerable<User> users = DataModel.GetAllUsers();
            ObservableCollection<UserView> result = new ObservableCollection<UserView>();

            if (users == null)
            {
                return result;
            }

            IEnumerable<User> managers = from manager in users
                                         where UserRoleConverter.ConvertBinaryToRole(manager.Role) == UserRole.Manager
                                         select manager;

            foreach (User manager in managers)
            {
                result.Add(new UserView(manager));
            }

            return result;
        }

        /// <summary>
        /// Sets the current project, sprint, and story from a selected task
        /// </summary>
        /// <param name="task">The selected task</param>
        public void JumpToTask(TaskView task)
        {
            if (task == null) // Bad input value
            {
                throw new ArgumentNullException("Arguments to JumpToTask must not be null");
            }

            CurrStory = new StoryView(DataModel.GetStoryByID(task.StoryID));
            CurrSprint = new SprintView(DataModel.GetSprintByID(CurrStory.SprintID));
            CurrProject = new ProjectView(DataModel.GetProjectByID(CurrSprint.ProjectID));
            CurrTask = task;
        }

        /// <summary>
        /// Creates a new team
        /// </summary>
        /// <param name="name">The name of the new team</param>
        /// <param name="manager">The manager of the new team</param>
        /// <param name="lead">The team lead for the new team</param>
        /// <returns>True if the add succeeds, false otherwise</returns>
        public bool AddTeam(string name, UserView manager, UserView lead)
        {
            if (!_isLoggedIn)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (manager == null || lead == null || name == null)
            {
                throw new ArgumentNullException("Arguments to AddTeam must not be null");
            }

            User managerUser = DataModel.GetUserByID(manager.UserId);
            User leadUser = DataModel.GetUserByID(lead.UserId);

            Team newTeam = new Team()
            {
                Manager = manager.UserId,
                ManagerUser = managerUser,
                Project = new System.Data.Linq.EntitySet<Project>(),
                Team_ = new System.Data.Linq.EntitySet<User>() { leadUser },
                Team_lead = lead.UserId,
                Team_name = name,
                User = leadUser
            };

            UpdateAllTeams();
            return DataModel.CommitChanges();
        }

        /// <summary>
        /// Creates a new project
        /// </summary>
        /// <param name="name">The name of the project</param>
        /// <param name="startDate">The start date of the project</param>
        /// <param name="endDate">The end date of the project</param>
        /// <param name="owner">The User who owns the new project</param>
        /// <param name="team">The team responsible for the new project</param>
        /// <returns>True if the add succeeds, false otherwise</returns>
        public bool AddProject(string name, DateTime startDate, Nullable<DateTime> endDate, UserView owner, TeamView team)
        {
            if (!_isLoggedIn)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (name == null || startDate == null || owner == null || team == null)
            {
                throw new ArgumentNullException("Arguments to AddProject must not be null");
            }

            User ownerUser = DataModel.GetUserByID(owner.UserId);
            Team projectTeam = DataModel.GetTeamByID(team.TeamID);

            Project newProject = new Project()
            {
                Project_name = name,
                Start_date = startDate,
                End_date = endDate,
                Owner = owner.UserId,
                User = ownerUser,
                Team_id = team.TeamID,
                Team = projectTeam,
                Sprint = new System.Data.Linq.EntitySet<Sprint>()
            };

            bool result = DataModel.CommitChanges();

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
                result &= DataModel.CommitChanges();
            }

            UpdateProjectsForUser();
            return result;
        }

        /// <summary>
        /// Creates a new sprint
        /// </summary>
        /// <param name="name">The name of the sprint</param>
        /// <param name="startDate">The start date of the sprint</param>
        /// <param name="endDate">The end date of the sprint</param>
        /// <returns>True if the add succeeds, false otherwise</returns>
        public bool AddSprint(string name, DateTime startDate, Nullable<DateTime> endDate)
        {
            if (!_isLoggedIn)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (startDate == null || name == null)
            {
                throw new ArgumentNullException("Arguments to AddSprint must not be null");
            }

            Project curr = DataModel.GetProjectByID(CurrProject.ProjectID);

            Sprint newSprint = new Sprint()
            {
                Sprint_name = name,
                Start_date = startDate,
                End_date = endDate,
                Project_id = curr.Project_id,
                Project = curr,
                Story = new System.Data.Linq.EntitySet<Story>()
            };

            bool result = DataModel.CommitChanges();
            UpdateSprintsForProject();

            return result;
        }

        /// <summary>
        /// Creates a new user story
        /// </summary>
        /// <param name="priority">The priority number for this story</param>
        /// <param name="text">The text of this story</param>
        /// <returns>True if the add succeeds, false otherwise</returns>
        public bool AddStory(int priority, string text)
        {
            if (!_isLoggedIn)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (priority < 0)
            {
                throw new ArgumentOutOfRangeException("Story priority must be nonnegative");
            }
            else if (text == null)
            {
                throw new ArgumentNullException("Arguments to AddStory must not be null");
            }

            Sprint curr = DataModel.GetSprintByID(CurrSprint.SprintID);

            Story newStory = new Story()
            {
                Priority_num = priority,
                Sprint_id = curr.Sprint_id,
                Sprint = curr,
                Text = text,
                Task = new System.Data.Linq.EntitySet<Task>()
            };

            bool result = DataModel.CommitChanges();
            UpdateStoriesForSprint();

            return result;
        }

        /// <summary>
        /// Creates a new task
        /// </summary>
        /// <param name="text">The text of this task</param>
        /// <param name="size">The size complexity of this task</param>
        /// <param name="value">The business value of this task</param>
        /// <param name="owner"></param>
        /// <param name="type"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool AddTask(String text, int size, int value, UserView owner, TaskType type, TaskState state)
        {
            if (!_isLoggedIn)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (!ComplexityValues.businessValue.Contains(value) || !ComplexityValues.sizeComplexity.Contains(size))
            {
                throw new ArgumentOutOfRangeException("Invalid complexity value");
            }
            else if (text == null)
            {
                throw new ArgumentNullException("Arguments to AddTask must not be null");
            }

            Story curr = DataModel.GetStoryByID(CurrStory.StoryID);
            User ownerUser = null;
            int? ownerId = null;

            if (owner != null)
            {
                ownerUser = DataModel.GetUserByID(owner.UserId);
                ownerId = owner.UserId;
            }

            Task newTask = new Task()
            {
                Text = text,
                Business_value = value,
                Size_complexity = size,
                Completion_date = null,
                Owner = ownerId,
                State = TaskStateConverter.ConvertToBinary(state),
                Type = TaskTypeConverter.ConvertToBinary(type),
                Story = curr,
                Story_id = curr.Story_id,
                User = ownerUser
            };

            bool result = DataModel.CommitChanges();
            UpdateTasksForStory();

            return result; 
        }

        /// <summary>
        /// Hashes a user's password using SHA1
        /// </summary>
        /// <param name="password">The user's password</param>
        /// <returns>The hash of the password</returns>
        private string hashPassword(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
    }
}
