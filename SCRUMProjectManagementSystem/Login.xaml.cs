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
                MainWindow main = new MainWindow(viewModel);
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "Unable to log in. Please check your username and password and try again.  If the problem persists, please contact your administrator.", 
                    "Login Failed", 
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
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
