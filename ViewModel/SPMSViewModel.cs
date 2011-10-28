using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            CurrTeam = new TeamView(curr.Team_);
            _isLoggedIn = true;

            UpdateProjectsForUser();
            UpdateTasksForUser();

            return true;
        }

        /// <summary>
        /// Updates the ProjectsForUser collection
        /// </summary>
        /// <returns>True if the update succeeds, false otherwise</returns>
        public bool UpdateProjectsForUser()
        {
            _projectsForUser.Clear(); // Clear the existing entries

            if (!_isLoggedIn) // No one is logged in
            {
                return false;
            }

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
            _sprintsForProject.Clear(); // Clear the existing entries

            if (!_isLoggedIn || CurrProject == null) // No one is logged in or a project has not been selected
            {
                return false;
            }

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
            _storiesForSprint.Clear(); // Clear the existing entries

            if (!_isLoggedIn || CurrSprint == null) // No one is logged in or a sprint has not been selected
            {
                return false;
            }

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
            _tasksForStory.Clear(); // Clear the existing entries

            if (!_isLoggedIn || CurrStory == null) // No one is logged in or a user story has not been selected
            {
                return false;
            }

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
        /// Updates the TasksForUser collection
        /// </summary>
        /// <returns>True if the update succeeds, false otherwise</returns>
        public bool UpdateTasksForUser()
        {
            _tasksForUser.Clear(); // Clear the existing entries

            if (!_isLoggedIn) // No one is logged in
            {
                return false;
            }

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
