using System.Windows;
using System.Windows.Controls;

namespace AttendanceApp
{
    public partial class StudentsWindow : Window
    {
        public StudentsWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            StudentsDataGrid.ItemsSource = null;
            StudentsDataGrid.ItemsSource = Database.GetStudents();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
 
            var newStudent = new Student { FullName = "Новый студент", GroupId = 1 };
            Database.AddStudent(newStudent);
            LoadData();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn?.DataContext is Student student)
            {
                if (MessageBox.Show($"Удалить студента {student.FullName}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Database.DeleteStudent(student);
                    LoadData();
                }
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данные сохранены!", "Успех");
        }
    }
}