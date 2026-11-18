using System;
using System.Linq;
using System.Windows;

namespace AttendanceApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ApplyRoleRules();
            UpdateDate();
        }

        private void UpdateDate()
        {
            string[] days = { "Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };
            DateTime now = DateTime.Now;
            TxtDate.Text = $"{days[(int)now.DayOfWeek]}, {now:dd MMMM yyyy, HH:mm}";
        }

        private void ApplyRoleRules()
        {
            if (Database.CurrentUser == null) return;

            if (Database.CurrentUser.Role == "Студент")
            {
                TxtWelcome.Text = $"Добро пожаловать, Студент ({Database.CurrentUser.Login})!";

                BtnDisciplines.Visibility = Visibility.Collapsed;
                BtnReports.Visibility = Visibility.Collapsed;
                BtnSettings.Visibility = Visibility.Collapsed;
                BtnStudents.Visibility = Visibility.Collapsed;
                BtnGroups.Visibility = Visibility.Collapsed;

                StudentChart.Visibility = Visibility.Visible;
                TeacherChart.Visibility = Visibility.Collapsed;
                TeacherQuickActions.Visibility = Visibility.Collapsed;
                StudentQuickActions.Visibility = Visibility.Visible;
                TeacherBottomPanel.Visibility = Visibility.Collapsed;

                TxtStatsTitle.Text = $"Статистика группы {Database.CurrentUser.GroupName}";

                int studentCount = Database.Students.Count(s => s.GroupName == Database.CurrentUser.GroupName);
                TxtStat1Name.Text = "Человек в группе";
                TxtStat1Value.Text = studentCount.ToString();

                TxtStat2Name.Text = "Дисциплин всего";
                TxtStat2Value.Text = Database.Disciplines.Count.ToString();

                TxtStat3Name.Text = "Долгов";
                TxtStat3Value.Text = "2";

                TxtStat4Name.Text = "Посещаемость";
                TxtStat4Value.Text = "85%";
            }
            else
            {
                TxtWelcome.Text = $"Добро пожаловать, Преподаватель ({Database.CurrentUser.Login})!";
                TxtTeacherName.Text = $"Преподаватель: {Database.CurrentUser.Login}";

                StudentChart.Visibility = Visibility.Collapsed;
                TeacherChart.Visibility = Visibility.Visible;
                TeacherQuickActions.Visibility = Visibility.Visible;
                StudentQuickActions.Visibility = Visibility.Collapsed;
                TeacherBottomPanel.Visibility = Visibility.Visible;

                TxtStatsTitle.Text = "Краткая статистика";
                TxtStat1Name.Text = "Студентов";
                TxtStat1Value.Text = Database.Students.Count.ToString();
                TxtStat2Name.Text = "Групп";
                TxtStat2Value.Text = Database.Groups.Count.ToString();
                TxtStat3Name.Text = "Дисциплин";
                TxtStat3Value.Text = Database.Disciplines.Count.ToString();
                TxtStat4Name.Text = "Задолженностей";
                TxtStat4Value.Text = "3";
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnStudents_Click(object sender, RoutedEventArgs e)
        {
            var win = new StudentsWindow();
            win.Owner = this;
            win.ShowDialog();
            ApplyRoleRules();
        }

        private void BtnGroups_Click(object sender, RoutedEventArgs e)
        {
            var win = new GroupsWindow();
            win.Owner = this;
            win.ShowDialog();
            ApplyRoleRules();
        }

        private void BtnDisciplines_Click(object sender, RoutedEventArgs e)
        {
            var win = new DisciplinesWindow();
            win.Owner = this;
            win.ShowDialog();
            ApplyRoleRules();
        }
    }
}