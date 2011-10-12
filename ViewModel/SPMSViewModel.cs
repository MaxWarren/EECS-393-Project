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

        private ObservableCollection<Project> _projectsForUser;
        private ObservableCollection<Task> _tasksForUser;
        private ObservableCollection<Sprint> _sprintsForProject;
        private ObservableCollection<Story> _storiesForSprint;
        private ObservableCollection<Task> _tasksForStory;

        /// <summary>
        /// The currently logged in user
        /// </summary>
        public User CurrUser { get; private set; }

        /// <summary>
        /// The team to which the current user belongs
        /// </summary>
        public Team CurrTeam { get; private set; }

        /// <summary>
        /// The project most recently selected by the user
        /// </summary>
        public Project CurrProject { get; private set; }

        /// <summary>
        /// The sprint most recently selected by the user
        /// </summary>
        public Sprint CurrSprint { get; private set; }

        /// <summary>
        /// The user story most recently selected by the user
        /// </summary>
        public Story CurrStory { get; private set; }

        /// <summary>
        /// The task most recently selected by the user
        /// </summary>
        public Task CurrTask { get; private set; }

        /// <summary>
        /// A list of all projects that belong to the team to which the current user belongs
        /// </summary>
        public ObservableCollection<Project> ProjectsForUser
        {
            get { return _projectsForUser; }
            private set { _projectsForUser = value; }
        }

        /// <summary>
        /// A list of all tasks assigned to the current user
        /// </summary>
        public ObservableCollection<Task> TasksForUser
        {
            get { return _tasksForUser; }
            private set { _tasksForUser = value; }
        }

        /// <summary>
        /// A list of all sprints that make up the current project
        /// </summary>
        public ObservableCollection<Sprint> SprintsForProject
        {
            get { return _sprintsForProject; }
            private set { _sprintsForProject = value; }
        }

        /// <summary>
        /// A list of all user stories belonging to the current sprint
        /// </summary>
        public ObservableCollection<Story> StoriesForSprint
        {
            get { return _storiesForSprint; }
            private set { _storiesForSprint = value; }
        }

        /// <summary>
        /// A list of all tasks belonging to the current user story
        /// </summary>
        public ObservableCollection<Task> TasksForStory
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
            _projectsForUser = new ObservableCollection<Project>();
            _sprintsForProject = new ObservableCollection<Sprint>();
            _storiesForSprint = new ObservableCollection<Story>();
            _tasksForStory = new ObservableCollection<Task>();
            _tasksForUser = new ObservableCollection<Task>();
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
            Console.WriteLine(passHash);
            User curr = DataModel.AuthenticateUser(userId, passHash);

            if (curr == null) //  Authentication failed
            {
                return false;
            }

            CurrUser = curr; // Store the user
            CurrTeam = curr.Team;
            _isLoggedIn = true;

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
                _projectsForUser.Add(p);
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
                _sprintsForProject.Add(s);
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
                _storiesForSprint.Add(s);
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
                _tasksForStory.Add(t);
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

            IEnumerable<Task> tasks = DataModel.GetTasksForUser(CurrUser.UserID);
            if (tasks == null) // An error occured
            {
                return false;
            }

            foreach (Task t in tasks)
            {
                _tasksForUser.Add(t);
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
