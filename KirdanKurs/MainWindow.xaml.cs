using System;
using System.Windows;

namespace AttendanceApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            TxtDate.Text = "Сегодня: " + DateTime.Now.ToString("dd.MM.yyyy");

            TxtStudentsCount.Text = Database.GetStudentsCount().ToString();
            TxtGroupsCount.Text = "6";
            TxtDisciplinesCount.Text = "14";
            TxtDebtsCount.Text = Database.GetNotifications().Count.ToString();

            TxtCurrentUser.Text = App.CurrentUser ?? "Преподаватель";
            TxtRole.Text = "Роль: " + (App.CurrentRole ?? "преподаватель");
            TxtGreeting.Text = $"Добро пожаловать, {App.CurrentUser ?? "преподаватель"}!";

            GridNotifications.ItemsSource = Database.GetNotifications();
        }

        private void BtnStudents_Click(object sender, RoutedEventArgs e) => OpenWindow(new StudentsWindow());
        private void BtnGroups_Click(object sender, RoutedEventArgs e) => OpenWindow(new GroupsWindow());
        private void BtnDisciplines_Click(object sender, RoutedEventArgs e) => OpenWindow(new DisciplinesWindow());
        private void BtnAttendance_Click(object sender, RoutedEventArgs e) => OpenWindow(new AttendanceWindow());
        private void BtnGrades_Click(object sender, RoutedEventArgs e) => OpenWindow(new GradesWindow());
        private void BtnReports_Click(object sender, RoutedEventArgs e) => OpenWindow(new ReportsWindow());
        private void BtnSettings_Click(object sender, RoutedEventArgs e) => OpenWindow(new SettingsWindow());

        private void OpenWindow(Window w)
        {
            w.Owner = this;
            w.ShowDialog();
            LoadDashboard();
        }

        private void BtnBackup_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Файл базы данных (app.db) находится в папке с программой.\nСкопируйте его для резервной копии.", "Резервная копия");
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Выйти из приложения?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }
    }

    public class Notification
    {
        public string? StudentName { get; set; }
        public string? GroupName { get; set; }
        public string? Discipline { get; set; }
        public int MissedCount { get; set; }
        public string? DebtInfo { get; set; }
    }
}