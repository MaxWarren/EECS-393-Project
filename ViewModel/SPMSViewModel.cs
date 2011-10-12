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
        private int _currUser;
        private ObservableCollection<Project> _projectsForUser;
        private ObservableCollection<Task> _tasksForUser;
        private ObservableCollection<Sprint> _sprintsForProject;
        private ObservableCollection<Story> _storiesForSprint;
        private ObservableCollection<Task> _tasksForStory;

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

            _currUser = curr.UserID; // Store the user ID
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
