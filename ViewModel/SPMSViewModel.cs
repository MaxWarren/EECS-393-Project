using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;

namespace ViewModel
{
    /// <summary>
    /// SPMS View Model
    /// </summary>
    public class SPMSViewModel
    {
        private ObservableCollection<Project> _projectsForUser;
        private ObservableCollection<Task> _tasksForUser;
        private ObservableCollection<Sprint> _sprintsForProject;
        private ObservableCollection<Story> _storiesForSprint;
        private ObservableCollection<Task> _tasksForStory;

        /// <summary>
        /// The currently logged in user
        /// </summary>
        public User CurrUser { get; set; }

        /// <summary>
        /// The team to which the current user belongs
        /// </summary>
        public Team CurrTeam { get; set; }

        /// <summary>
        /// The project most recently selected by the user
        /// </summary>
        public Project CurrProject { get; set; }

        /// <summary>
        /// The sprint most recently selected by the user
        /// </summary>
        public Sprint CurrSprint { get; set; }

        /// <summary>
        /// The user story most recently selected by the user
        /// </summary>
        public Sprint CurrStory { get; set; }

        /// <summary>
        /// The task most recently selected by the user
        /// </summary>
        public Sprint CurrTask { get; set; }

        /// <summary>
        /// A list of all projects that belong to the team to which the current user belongs
        /// </summary>
        public ObservableCollection<Project> ProjectsForUser
        {
            get { return _projectsForUser; }
            set { _projectsForUser = value; }
        }

        /// <summary>
        /// A list of all tasks assigned to the current user
        /// </summary>
        public ObservableCollection<Task> TasksForUser
        {
            get { return _tasksForUser; }
            set { _tasksForUser = value; }
        }

        /// <summary>
        /// A list of all sprints that make up the current project
        /// </summary>
        public ObservableCollection<Sprint> SprintsForProject
        {
            get { return _sprintsForProject; }
            set { _sprintsForProject = value; }
        }

        /// <summary>
        /// A list of all user stories belonging to the current sprint
        /// </summary>
        public ObservableCollection<Story> StoriesForSprint
        {
            get { return _storiesForSprint; }
            set { _storiesForSprint = value; }
        }

        /// <summary>
        /// A list of all tasks belonging to the current user story
        /// </summary>
        public ObservableCollection<Task> TasksForStory
        {
            get { return _tasksForStory; }
            set { _tasksForStory = value; }
        }

        /// <summary>
        /// Initializes the view model
        /// </summary>
        public SPMSViewModel()
        {
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
            User curr = DatabaseLayer.DatabaseLayer.AuthenticateUser(userId, passHash);

            if (curr == null) //  Authentication failed
            {
                return false;
            }

            CurrUser = curr; // Store the user
            CurrTeam = curr.Team;
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
