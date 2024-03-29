﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
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
        #region Private fields
        private IDataModel _dataModel;

        /// <summary>
        /// Indicates whether or not a user is logged in
        /// </summary>
        private bool _isLoggedIn;

        private UserView _currUser;
        private TeamView _currTeam;
        private ProjectView _currProject;
        private SprintView _currSprint;
        private StoryView _currStory;
        private TaskView _currTask;

        private ObservableCollection<ProjectView> _projectsForUser;
        private ObservableCollection<TaskView> _tasksForUser;
        private ObservableCollection<SprintView> _sprintsForProject;
        private ObservableCollection<StoryView> _storiesForSprint;
        private ObservableCollection<TaskView> _tasksForStory;
        private ObservableCollection<TeamView> _allTeams;
        #endregion

        #region Current State Properties
        /// <summary>
        /// Indicates if the current user is a manager
        /// </summary>
        public bool IsManager { get; set; }

        /// <summary>
        /// Indicates whether or not the client is in "historic view"
        /// </summary>
        public bool HistoricMode { get; set; }

        /// <summary>
        /// The currently logged in user
        /// </summary>
        public UserView CurrUser
        {
            get { return _currUser; }
            set { _currUser = value; updateTasksForUser(); }
        }

        /// <summary>
        /// The team to which the current user belongs
        /// </summary>
        public TeamView CurrTeam
        {
            get { return _currTeam; }
            set { _currTeam = value; updateProjectsForUser(); }
        }

        /// <summary>
        /// The project most recently selected by the user
        /// </summary>
        public ProjectView CurrProject
        {
            get { return _currProject; }
            set { _currProject = value; updateSprintsForProject(); }
        }

        /// <summary>
        /// The sprint most recently selected by the user
        /// </summary>
        public SprintView CurrSprint
        {
            get { return _currSprint; }
            set { _currSprint = value; updateStoriesForSprint(); }
        }

        /// <summary>
        /// The user story most recently selected by the user
        /// </summary>
        public StoryView CurrStory
        {
            get { return _currStory; }
            set { _currStory = value; updateTasksForStory(); }
        }

        /// <summary>
        /// The task most recently selected by the user
        /// </summary>
        public TaskView CurrTask
        {
            get { return _currTask; }
            set { _currTask = value; }
        }
        #endregion

        #region Public ObservableCollection Properties
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
            get { updateAllTeams(); return _allTeams; }
            private set { _allTeams = value; }
        }

        /// <summary>
        /// A list of all managers in the database
        /// </summary>
        public ObservableCollection<UserView> AllManagers
        {
            get { return getManagers(); }
        }

        /// <summary>
        /// A list of all members of the current team
        /// </summary>
        public ObservableCollection<UserView> CurrTeamMembers
        {
            get { return GetTeamMembers(CurrTeam).Item1; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes the view model with default DataModel
        /// </summary>
        public SPMSViewModel() : this(DataModel.Instance) { }

        /// <summary>
        /// Initializes the view model
        /// </summary>
        /// <param name="dataModel">The IDataModel to use in the view model</param>
        public SPMSViewModel(IDataModel dataModel)
        {
            _dataModel = dataModel;
            _isLoggedIn = false;

            // Set all the observable collections to empty lists
            _projectsForUser = new ObservableCollection<ProjectView>();
            _sprintsForProject = new ObservableCollection<SprintView>();
            _storiesForSprint = new ObservableCollection<StoryView>();
            _tasksForStory = new ObservableCollection<TaskView>();
            _tasksForUser = new ObservableCollection<TaskView>();
            _allTeams = new ObservableCollection<TeamView>();
        }
        #endregion

        #region Miscellaneous
        /// <summary>
        /// Authenticates the user
        /// </summary>
        /// <param name="userId">The ID of the user to authenticate</param>
        /// <param name="password">The user's password</param>
        /// <returns>True if authentication succeeds, false otherwise</returns>
        public bool AuthenticateUser(int userId, string password)
        {
            string passHash = hashPassword(password);

            User curr = _dataModel.AuthenticateUser(userId, passHash);

            if (curr == null) //  Authentication failed
            {
                return false;
            }

            _isLoggedIn = true;

            CurrTeam = new TeamView(curr.Team_); // Store the team
            CurrUser = new UserView(curr); // Store the user

            IsManager = CurrUser.Role == UserRole.Manager;

            return true;
        }

        /// <summary>
        /// Sets the current project, sprint, and story from a selected task
        /// </summary>
        /// <param name="task">The selected task</param>
        public void JumpToTask(TaskView task)
        {
            if (!_isLoggedIn)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (task == null) // Bad input value
            {
                throw new ArgumentNullException("Arguments to JumpToTask must not be null");
            }

            CurrStory = new StoryView(_dataModel.GetStoryByID(task.StoryID));
            CurrSprint = new SprintView(_dataModel.GetSprintByID(CurrStory.SprintID));
            CurrProject = new ProjectView(_dataModel.GetProjectByID(CurrSprint.ProjectID));
            CurrTask = task;
        }

        /// <summary>
        /// Toggles whether or not the client is in historic mode
        /// </summary>
        public void ToggleHistoricMode()
        {
            HistoricMode = !HistoricMode; // Toggle the state
            updateProjectsForUser();
            updateTasksForUser();
        }

        /// <summary>
        /// Calculates and returns the burndown for the current sprint
        /// </summary>
        /// <returns>A tuple, where the first element is the ideal burndown and the second is the actual burndown</returns>
        public Tuple<IDictionary<DateTime, double>, IDictionary<DateTime, int>> GetCurrSprintBurndown()
        {
            if (!_isLoggedIn || CurrSprint == null)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (CurrSprint.StartDate > DateTime.Today)
            {
                throw new InvalidOperationException("The sprint must have started to view its burndown");
            }

            // If the sprint doesn't have an end date, calculate through the current day
            DateTime endDate = CurrSprint.EndDate.HasValue ? CurrSprint.EndDate.Value : DateTime.Today;
            DateTime startDate = CurrSprint.StartDate;
            TimeSpan days = endDate - startDate;

            IEnumerable<TaskView> allTasks = _dataModel.GetAllTasksForSprint(CurrSprint.SprintID).Select(t => new TaskView(t)); // Get all tasks for this sprint
            int numTasks = allTasks.Count();

            var result = new Tuple<IDictionary<DateTime, double>, IDictionary<DateTime, int>>(new Dictionary<DateTime, double>(), new Dictionary<DateTime, int>());

            for (int i = 0; i <= days.Days; i++)
            {
                DateTime date = startDate.AddDays(i);
                int actual = numTasks - allTasks.Where(t => (t.CompletionDate.HasValue && t.CompletionDate.Value <= date)).Count(); // Actual burndown
                double ideal = numTasks - i * ((double)numTasks / days.Days); // Ideal burndown

                result.Item1.Add(date, ideal);
                result.Item2.Add(date, actual);
            }

            return result;
        }

        public Dictionary<UserView, int[]> GetCurrSprintUserStatus()
        {
            if (!_isLoggedIn || CurrSprint == null)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (CurrSprint.StartDate > DateTime.Today)
            {
                throw new InvalidOperationException("The sprint must have started to view its task status");
            }

            IEnumerable<User> users = _dataModel.GetTeamMembers(_dataModel.GetProjectByID(CurrSprint.ProjectID).Team_id);
            IEnumerable<Task> allTasks = _dataModel.GetAllTasksForSprint(CurrSprint.SprintID);
            
            Dictionary<UserView, int[]> results = new Dictionary<UserView, int[]>();
            
            foreach (User u in users)
            {
                IEnumerable<TaskView> tasks = allTasks.Where(task => task.Owner.HasValue && task.Owner.Value == u.User_id).Select(t => new TaskView(t));

                UserView user = new UserView(u);
                int[] vals = 
                {
                    tasks.Count(t => t.State == TaskState.Completed),
                    tasks.Count(t => t.State == TaskState.In_Progress),
                    tasks.Count(t => t.State == TaskState.Blocked),
                };

                results.Add(user, vals);
            }

            return results;
        }
        #endregion

        #region Get Lists of Users
        /// <summary>
        /// Gets a list of users in a team and a list of users not in a team
        /// </summary>
        /// <param name="team">The team for which to search</param>
        /// <returns>A tuple, the first element of which is the list of team members and the second is the list of Users not in the team</returns>
        public Tuple<ObservableCollection<UserView>, ObservableCollection<UserView>> GetTeamMembers(TeamView team)
        {
            if (!_isLoggedIn)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (team == null) // Bad value
            {
                throw new ArgumentNullException("Arguments to GetTeamMembers must not be null");
            }

            var result = new Tuple<ObservableCollection<UserView>, ObservableCollection<UserView>>(
                new ObservableCollection<UserView>(),
                new ObservableCollection<UserView>());

            IEnumerable<User> members = _dataModel.GetTeamMembers(team.TeamID);
            IEnumerable<User> nonMembers = _dataModel.GetUsersNotInTeam(team.TeamID);

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
        #endregion

        #region Get Entities By ID
        /// <summary>
        /// Gets the Team for a User
        /// </summary>
        /// <param name="user">The User for which to get the team</param>
        /// <returns>The Team to which the given user belongs</returns>
        public TeamView GetTeamForUser(UserView user)
        {
            if (!_isLoggedIn)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (user == null)
            {
                throw new ArgumentNullException("Arguments to GetUserTeam must not be null");
            }

            Team team = _dataModel.GetTeamByID(user.TeamId);

            if (team != null)
            {
                return new TeamView(team);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a User by their id
        /// </summary>
        /// <param name="userId">The user id for which to search</param>
        /// <returns>The user if one exists with that id</returns>
        public UserView GetUserByID(int userId)
        {
            if (!_isLoggedIn)
            {
                throw new InvalidOperationException("User is not logged in");
            }
            else if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException("userID must be positive");
            }

            User user = _dataModel.GetUserByID(userId);

            if (user != null)
            {
                return new UserView(user);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a Team by its id
        /// </summary>
        /// <param name="teamId">The team id for which to search</param>
        /// <returns>The team if one exists with that id</returns>
        public TeamView GetTeamByID(int teamId)
        {
            if (!_isLoggedIn)
            {
                throw new InvalidOperationException("User is not logged in");
            }
            else if (teamId <= 0)
            {
                throw new ArgumentOutOfRangeException("teamID must be positive");
            }

            Team team = _dataModel.GetTeamByID(teamId);

            if (team != null)
            {
                return new TeamView(team);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Change Existing Entities
        /// <summary>
        /// Moves a user to a given team
        /// </summary>
        /// <param name="user">The user to change</param>
        /// <param name="team">The team to which to move the user</param>
        /// <returns>True if moving the user succeeds, false otherwise</returns>
        public bool MoveUserToTeam(UserView user, TeamView team)
        {
            if (!_isLoggedIn)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            if (user == null || team == null)
            {
                throw new ArgumentNullException("Arguments to ChangeTeam must not be null");
            }

            bool result = _dataModel.MoveUserToTeam(user.UserID, team.TeamID);

            if (result && user.UserID == CurrUser.UserID)
            {
                CurrTeam = team;
                updateProjectsForUser();
            }

            return result;
        }

        /// <summary>
        /// Updates the current project
        /// </summary>
        /// <param name="name">The name of the project</param>
        /// <param name="startDate">The start date of the project</param>
        /// <param name="endDate">The end date of the project</param>
        /// <param name="owner">The User who owns the new project</param>
        /// <param name="team">The team responsible for the new project</param>
        /// <returns>True if the changes succeed, false otherwise</returns>
        public bool ChangeCurrProject(string name, Nullable<DateTime> startDate, Nullable<DateTime> endDate, UserView owner, TeamView team)
        {
            if (!_isLoggedIn || CurrProject == null)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (name == null || !startDate.HasValue || owner == null || team == null)
            {
                throw new ArgumentNullException("Arguments to AddProject must not be null");
            }

            bool result = _dataModel.ChangeProject(CurrProject.ProjectID, name, startDate.Value, endDate, owner.UserID, team.TeamID);
            if (result)
            {
                updateProjectsForUser();
                CurrProject = new ProjectView(_dataModel.GetProjectByID(CurrProject.ProjectID));
            }

            return result;
        }

        /// <summary>
        /// Updates the current sprint
        /// </summary>
        /// <param name="name">The name of the sprint</param>
        /// <param name="startDate">The start date of the sprint</param>
        /// <param name="endDate">The end date of the sprint</param>
        /// <returns>True if the changes succeed, false otherwise</returns>
        public bool ChangeCurrSprint(string name, Nullable<DateTime> startDate, Nullable<DateTime> endDate)
        {
            if (!_isLoggedIn || CurrSprint == null)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (!startDate.HasValue || name == null)
            {
                throw new ArgumentNullException("Arguments to AddSprint must not be null");
            }

            bool result = _dataModel.ChangeSprint(CurrSprint.SprintID, name, startDate.Value, endDate);
            if (result)
            {
                CurrSprint = new SprintView(_dataModel.GetSprintByID(CurrSprint.SprintID));
                updateSprintsForProject();
            }

            return result;
        }

        /// <summary>
        /// Updates the current user story
        /// </summary>
        /// <param name="priority">The priority number for this story</param>
        /// <param name="text">The text of this story</param>
        /// <param name="sprint">The sprint in which to place this story</param>
        /// <returns>True if the changes succeed, false otherwise</returns>
        public bool ChangeCurrStory(string priority, string text, SprintView sprint)
        {
            if (!_isLoggedIn || CurrStory == null)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (priority == null || text == null || sprint == null)
            {
                throw new ArgumentNullException("Arguments to AddStory must not be null");
            }
            int p;
            if (!int.TryParse(priority, out p))
            {
                throw new ArgumentException("Priority must be a number");
            }
            if (p <= 0)
            {
                throw new ArgumentOutOfRangeException("Priority must be positive");
            }

            bool result = _dataModel.ChangeStory(CurrStory.StoryID, p, text, sprint.SprintID);
            if (result)
            {
                updateStoriesForSprint();
                CurrStory = new StoryView(_dataModel.GetStoryByID(CurrStory.StoryID));
            }

            return result;
        }

        /// <summary>
        /// Updates the current task
        /// </summary>
        /// <param name="text">The text of this task</param>
        /// <param name="size">The size complexity of this task</param>
        /// <param name="value">The business value of this task</param>
        /// <param name="owner">The user who owns this task</param>
        /// <param name="type">The type of this task</param>
        /// <param name="state">The state of this task</param>
        /// <param name="completion">The date this task was completed</param>
        /// <returns>True if the changes succeed, false otherwise</returns>
        public bool ChangeCurrTask(string text, int size, int value, UserView owner, TaskType type, TaskState state, Nullable<DateTime> completion)
        {
            if (!_isLoggedIn || CurrTask == null)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (owner == null && state != TaskState.Unassigned) // Giving an unassigned task any state but unassigned is not allowed
            {
                throw new InvalidOperationException("A task without an owner must be marked Unassigned");
            }
            else if (!EnumValues.businessValue.Contains(value) || !EnumValues.sizeComplexity.Contains(size))
            {
                throw new ArgumentOutOfRangeException("Invalid complexity value");
            }
            else if (text == null)
            {
                throw new ArgumentNullException("Arguments to AddTask must not be null");
            }

            bool result = _dataModel.ChangeTask(CurrTask.TaskID, text, size, value, owner == null ? null : new int?(owner.UserID), type.ConvertToBinary(), state.ConvertToBinary(), completion);
            if (result)
            {
                updateTasksForStory();
                updateTasksForUser();
                CurrTask = new TaskView(_dataModel.GetTaskByID(CurrTask.TaskID));
            }

            return result;
        }
        #endregion

        #region Create New Entities
        /// <summary>
        /// Creates a new team
        /// </summary>
        /// <param name="name">The name of the new team</param>
        /// <param name="manager">The manager of the new team</param>
        /// <param name="lead">The team lead for the new team</param>
        /// <returns>True if creating the team succeeds, false otherwise</returns>
        public bool CreateTeam(string name, UserView manager, UserView lead)
        {
            if (!_isLoggedIn)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (manager == null || lead == null || name == null)
            {
                throw new ArgumentNullException("Arguments to AddTeam must not be null");
            }

            return _dataModel.CreateTeam(name, manager.UserID, lead.UserID);
        }

        /// <summary>
        /// Creates a new project
        /// </summary>
        /// <param name="name">The name of the project</param>
        /// <param name="startDate">The start date of the project</param>
        /// <param name="endDate">The end date of the project</param>
        /// <param name="owner">The User who owns the new project</param>
        /// <param name="team">The team responsible for the new project</param>
        /// <returns>True if creating the project succeeds, false otherwise</returns>
        public bool CreateProject(string name, Nullable<DateTime> startDate, Nullable<DateTime> endDate, UserView owner, TeamView team)
        {
            if (!_isLoggedIn)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (name == null || owner == null || team == null || !startDate.HasValue)
            {
                throw new ArgumentNullException("Arguments to AddProject must not be null");
            }

            bool result = _dataModel.CreateProject(name, startDate.Value, endDate, owner.UserID, team.TeamID);
            updateProjectsForUser();

            return result;
        }

        /// <summary>
        /// Creates a new sprint
        /// </summary>
        /// <param name="name">The name of the sprint</param>
        /// <param name="startDate">The start date of the sprint</param>
        /// <param name="endDate">The end date of the sprint</param>
        /// <returns>True if creating the sprint succeeds, false otherwise</returns>
        public bool CreateSprint(string name, Nullable<DateTime> startDate, Nullable<DateTime> endDate)
        {
            if (!_isLoggedIn || CurrProject == null)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (!startDate.HasValue || name == null)
            {
                throw new ArgumentNullException("Arguments to AddSprint must not be null");
            }

            bool result = _dataModel.CreateSprint(name, startDate.Value, endDate, CurrProject.ProjectID);
            updateSprintsForProject();

            return result;
        }

        /// <summary>
        /// Creates a new user story
        /// </summary>
        /// <param name="priority">The priority number for this story</param>
        /// <param name="text">The text of this story</param>
        /// <returns>True if creating the story succeeds, false otherwise</returns>
        public bool CreateStory(string priority, string text)
        {
            if (!_isLoggedIn || CurrSprint == null)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (text == null || priority == null)
            {
                throw new ArgumentNullException("Arguments to AddStory must not be null");
            }
            int p;
            if (!int.TryParse(priority, out p))
            {
                throw new ArgumentException("Priority must be a number");
            }
            if (p <= 0)
            {
                throw new ArgumentOutOfRangeException("Priority must be positive");
            }

            bool result = _dataModel.CreateStory(p, text, CurrSprint.SprintID);
            updateStoriesForSprint();

            return result;
        }

        /// <summary>
        /// Creates a new task
        /// </summary>
        /// <param name="text">The text of this task</param>
        /// <param name="size">The size complexity of this task</param>
        /// <param name="value">The business value of this task</param>
        /// <param name="owner">The user who owns this task</param>
        /// <param name="type">The type of this task</param>
        /// <param name="state">The state of this task</param>
        /// <returns>True if creating the task succeeds, false otherwise</returns>
        public bool CreateTask(string text, int size, int value, UserView owner, TaskType type, TaskState state, Nullable<DateTime> completionDate)
        {
            if (!_isLoggedIn || CurrStory == null)
            {
                throw new InvalidOperationException("User must be logged in");
            }
            else if (owner == null && state != TaskState.Unassigned) // Giving an unassigned task any state but unassigned is not allowed
            {
                throw new InvalidOperationException("A task without an owner must be marked Unassigned");
            }
            else if (!EnumValues.businessValue.Contains(value) || !EnumValues.sizeComplexity.Contains(size))
            {
                throw new ArgumentOutOfRangeException("Invalid complexity value");
            }
            else if ((state == TaskState.Completed && !completionDate.HasValue) || (state != TaskState.Completed && completionDate.HasValue))
            {
                throw new InvalidOperationException("A task has a completion date iff it is completed");
            }
            else if (text == null)
            {
                throw new ArgumentNullException("Arguments to AddTask must not be null");
            }

            bool result = _dataModel.CreateTask(text, size, value, owner == null ? null : new int?(owner.UserID), type.ConvertToBinary(), state.ConvertToBinary(), CurrStory.StoryID, completionDate);
            updateTasksForStory();
            updateTasksForUser();

            return result;
        }
        #endregion

        #region Validation
        /// <summary>
        /// Checks if user input for a team is valid
        /// </summary>
        /// <param name="name">The name of the team</param>
        /// <param name="manager">The manager of the team</param>
        /// <param name="lead">The team lead</param>
        /// <returns>True if the data is valid, false otherwise</returns>
        public bool ValidateTeam(string name, UserView manager, UserView lead)
        {
            bool result = true;

            result &= (name != null && name.Length > 0 && name.Length <= 50); // Name between 1 and 50 characters
            result &= (manager != null); // Manager is required
            result &= (lead != null); // Team lead is required

            return result;
        }

        /// <summary>
        /// Checks if user input for a project is valid
        /// </summary>
        /// <param name="name">The name of the project</param>
        /// <param name="startDate">The start date of the project</param>
        /// <param name="endDate">The end date of the project</param>
        /// <param name="owner">The owner of the project</param>
        /// <param name="team">The team responsible for the project</param>
        /// <returns>True if the data is valid, false otherwise</returns>
        public bool ValidateProject(string name, Nullable<DateTime> startDate, Nullable<DateTime> endDate, UserView owner, TeamView team)
        {
            bool result = true;

            result &= (name != null && name.Length > 0 && name.Length <= 50); // Name between 1 and 50 characters
            result &= startDate.HasValue; // Start date is required
            if (startDate.HasValue && endDate.HasValue)
            {
                result &= (startDate.Value < endDate.Value); // Sprint must start before it ends
            }
            result &= (owner != null); // Owner is required
            result &= (team != null); // Team is required

            return result;
        }

        /// <summary>
        /// Checks if user input for a sprint is valid
        /// </summary>
        /// <param name="name">The name of the sprint</param>
        /// <param name="startDate">The start date of the sprint</param>
        /// <param name="endDate">The end date of the sprint</param>
        /// <returns>True if the data is valid, false otherwise</returns>
        public bool ValidateSprint(string name, Nullable<DateTime> startDate, Nullable<DateTime> endDate)
        {
            bool result = true;

            result &= (name != null && name.Length > 0 && name.Length <= 50); // Name between 1 and 50 characters
            result &= startDate.HasValue; // Start date is required

            if (startDate.HasValue)
            {
                result &= (startDate.Value >= CurrProject.StartDate); // Sprint must start during the project
                if (CurrProject.EndDate.HasValue)
                {
                    result &= (startDate.Value < CurrProject.EndDate.Value); // Sprint must start during the project
                }

                if (endDate.HasValue)
                {
                    result &= (endDate.Value > startDate.Value); // Sprint must end after it starts
                    if (CurrProject.EndDate.HasValue)
                    {
                        result &= (endDate.Value <= CurrProject.EndDate.Value); // Sprint must end during the project
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if user input for a story is valid
        /// </summary>
        /// <param name="priority">The priority of the story</param>
        /// <param name="text">The text of the sprint</param>
        /// <returns>True if the data is valid, false otherwise</returns>
        public bool ValidateStory(string priority, string text)
        {
            bool result = true;

            result &= (text != null && text.Length > 0); // Text is required

            int p;
            result &= int.TryParse(priority, out p); // Priority is required to be a number
            result &= (p > 0); // Priority must be positive

            return result;
        }

        public bool ValidateTask(string text, UserView owner, TaskType? type, int? size, int? value, Nullable<DateTime> completion, TaskState? state)
        {
            bool result = true;

            result &= (text != null && text.Length > 0); // Text is required
            result &= (type.HasValue); // Type is required
            result &= (size.HasValue && EnumValues.sizeComplexity.Contains(size.Value)); // Size complexity is required to be one of a set of values
            result &= (value.HasValue && EnumValues.businessValue.Contains(value.Value)); // Business value is required to be one of a set of values
            // If the owner is null, the state must be Unassigned.  Otherwise the state cannot be Unassigned
            result &= (state.HasValue && ((owner == null && state.Value == TaskState.Unassigned) || (owner != null && state.Value != TaskState.Unassigned)));
            result &= (state.HasValue && (state.Value != TaskState.Completed || completion.HasValue)); // A completed task must have a completed date

            if (CurrSprint != null && completion.HasValue)
            {
                result &= (completion.Value >= CurrSprint.StartDate); // Tasks must be completed within their sprint

                if (CurrSprint.EndDate.HasValue)
                {
                    result &= (completion.Value <= CurrSprint.EndDate.Value); // Tasks must be completed within their sprint
                }
            }

            return result;
        }
        #endregion

        #region Private methods
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

        /// <summary>
        /// Updates the ProjectsForUser collection
        /// </summary>
        /// <returns>True if the update succeeds, false otherwise</returns>
        private bool updateProjectsForUser()
        {
            if (!_isLoggedIn || CurrTeam == null) // No one is logged in
            {
                throw new InvalidOperationException("User must be logged in");
            }

            _projectsForUser.Clear(); // Clear the existing entries

            IEnumerable<Project> projects;
            if (!HistoricMode)
            {
                projects = _dataModel.GetProjectsByTeam(CurrTeam.TeamID);
            }
            else
            {
                projects = _dataModel.GetAllProjects();
            }

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
        private bool updateSprintsForProject()
        {
            if (!_isLoggedIn || CurrProject == null) // No one is logged in or a project has not been selected
            {
                throw new InvalidOperationException("User must be logged in and CurrProject must be set");
            }

            _sprintsForProject.Clear(); // Clear the existing entries

            IEnumerable<Sprint> sprints = _dataModel.GetSprintsForProject(CurrProject.ProjectID);
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
        private bool updateStoriesForSprint()
        {
            if (!_isLoggedIn || CurrSprint == null) // No one is logged in or a sprint has not been selected
            {
                throw new InvalidOperationException("User must be logged in and CurrSprint must be set");
            }

            _storiesForSprint.Clear(); // Clear the existing entries

            IEnumerable<Story> stories = _dataModel.GetStoriesForSprint(CurrSprint.SprintID);
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
        private bool updateTasksForStory()
        {
            if (!_isLoggedIn || CurrStory == null) // No one is logged in or a user story has not been selected
            {
                throw new InvalidOperationException("User must be logged in and CurrStory must be set");
            }

            _tasksForStory.Clear(); // Clear the existing entries

            IEnumerable<Task> tasks = _dataModel.GetTasksForStory(CurrStory.StoryID);
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
        /// Updates the TasksForUser collection
        /// </summary>
        /// <returns>True if the update succeeds, false otherwise</returns>
        private bool updateTasksForUser()
        {
            if (!_isLoggedIn) // No one is logged in
            {
                throw new InvalidOperationException("User must be logged in");
            }

            _tasksForUser.Clear(); // Clear the existing entries
            IEnumerable<Task> tasks;

            if (!HistoricMode)
            {
                tasks = _dataModel.GetTasksForUser(CurrUser.UserID);
            }
            else
            {
                tasks = _dataModel.GetAllTasks();
            }

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
        /// Updates the AllTeams collection
        /// </summary>
        /// <returns>True if the update succeeds, false otherwise</returns>
        private bool updateAllTeams()
        {
            if (!_isLoggedIn) // No one is logged in
            {
                throw new InvalidOperationException("User must be logged in");
            }

            _allTeams.Clear(); // Clear the existing entries

            IEnumerable<Team> teams = _dataModel.GetAllTeams();
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
        /// Get all managers in the database
        /// </summary>
        /// <returns>A list of all managers in the database</returns>
        private ObservableCollection<UserView> getManagers()
        {
            if (!_isLoggedIn)
            {
                throw new InvalidOperationException("User must be logged in");
            }

            IEnumerable<User> users = _dataModel.GetAllUsers();
            ObservableCollection<UserView> result = new ObservableCollection<UserView>();

            if (users == null)
            {
                return result;
            }

            IEnumerable<User> managers = from manager in users
                                         where (UserRole)manager.Role.ConvertToInt() == UserRole.Manager
                                         select manager;

            if (managers != null)
            {
                foreach (User manager in managers)
                {
                    result.Add(new UserView(manager));
                }
            }

            return result;
        }

        #endregion
    }
}