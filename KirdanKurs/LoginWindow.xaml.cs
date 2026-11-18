using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AttendanceApp
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            CmbGroup.ItemsSource = Database.Groups;
        }

        private void CmbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GroupPanel == null) return;

            var selectedItem = CmbRole.SelectedItem as ComboBoxItem;
            string role = selectedItem?.Content?.ToString() ?? "";

            if (role == "Студент")
                GroupPanel.Visibility = Visibility.Visible;
            else
                GroupPanel.Visibility = Visibility.Collapsed;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string login = TxtLogin.Text.Trim();
            string password = TxtPassword.Password.Trim();
            string role = (CmbRole.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Студент";
            string groupName = (CmbGroup.SelectedItem as Group)?.Name ?? "Не выбрана";

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните логин и пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var existingUser = Database.Users.FirstOrDefault(u => u.Login == login);

            if (existingUser == null)
            {
                var newUser = new User { Login = login, Password = password, Role = role, GroupName = groupName };
                Database.Users.Add(newUser);
                Database.CurrentUser = newUser;
                OpenMainWindow();
            }
            else
            {
                if (existingUser.Password == password)
                {
                    Database.CurrentUser = existingUser;
                    OpenMainWindow();
                }
                else
                {
                    MessageBox.Show("Неверный пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void OpenMainWindow()
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}