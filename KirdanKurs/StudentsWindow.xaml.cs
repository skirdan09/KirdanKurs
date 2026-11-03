using System.Windows;

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
            GridStudents.ItemsSource = Database.GetStudents();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtName.Text) || string.IsNullOrWhiteSpace(TxtGroup.Text))
            {
                MessageBox.Show("Введите ФИО и группу!", "Ошибка");
                return;
            }
            Database.AddStudent(TxtName.Text.Trim(), TxtGroup.Text.Trim());
            TxtName.Clear();
            TxtGroup.Clear();
            LoadData();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (GridStudents.SelectedItem is Student selected)
            {
                var result = MessageBox.Show($"Удалить {selected.FullName}?", "Подтверждение",
                    MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Database.DeleteStudent(selected.Id);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите студента для удаления.", "Внимание");
            }
        }
    }
}