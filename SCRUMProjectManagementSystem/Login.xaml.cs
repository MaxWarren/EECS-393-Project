using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;
using ViewModel;

namespace SCRUMProjectManagementSystem
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            login();
        }

        private void login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                login();
            }
        }

        private void login()
        {
            try
            {
                SPMSViewModel viewModel = new SPMSViewModel();

                // Attempt to extract the user id
                int userId;
                if (!int.TryParse(textBox1.Text, out userId))
                {
                    userId = 0; // Not a number
                }
                string password = passwordBox1.Password;

                if (viewModel.AuthenticateUser(userId, password))
                {
                    new MainWindow(viewModel).Visibility = Visibility.Visible;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Username / Password", "Login Failed", MessageBoxButton.OK);
                }
            }
            catch
            {
                MessageBox.Show("Database Error.  Could Not Log In.", "Login Failed", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Disallow non-numeric input to the user id field
        /// </summary>
        /// <param name="sender">Object sending the event</param>
        /// <param name="e">Other event arguments</param>
        private void textBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = e.Text.IsNonNumeric();
        }
    }
}
