using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AttendanceApp
{
    public partial class DisciplinesWindow : Window
    {
        public DisciplinesWindow()
        {
            InitializeComponent();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            DisciplinesDataGrid.ItemsSource = null;
            DisciplinesDataGrid.ItemsSource = Database.Disciplines;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new Discipline { Id = Database.Disciplines.Count + 1, Code = "Новый код", Name = "Название", Category = "МДК", Teacher = "ФИО", Hours = 0 };
            Database.Disciplines.Add(newItem);
            UpdateGrid();

            DisciplinesDataGrid.SelectedItem = newItem;
            DisciplinesDataGrid.ScrollIntoView(newItem);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            DisciplinesDataGrid.CommitEdit();
            DisciplinesDataGrid.CommitEdit();
            MessageBox.Show("Данные успешно сохранены в общую базу!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn?.DataContext is Discipline item)
            {
                DisciplinesDataGrid.SelectedItem = item;
                DisciplinesDataGrid.CurrentCell = new DataGridCellInfo(item, DisciplinesDataGrid.Columns[0]);
                DisciplinesDataGrid.BeginEdit();
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn?.DataContext is Discipline item)
            {
                if (MessageBox.Show($"Удалить \"{item.Name}\"?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Database.Disciplines.Remove(item);
                    UpdateGrid();
                }
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            string category = (sender as Button).Tag.ToString();
            if (category == "Все")
                DisciplinesDataGrid.ItemsSource = Database.Disciplines;
            else
                DisciplinesDataGrid.ItemsSource = Database.Disciplines.Where(d => d.Category == category).ToList();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                DisciplinesDataGrid.ItemsSource = Database.Disciplines;
                return;
            }
            DisciplinesDataGrid.ItemsSource = Database.Disciplines.Where(d =>
                (d.Name != null && d.Name.ToLower().Contains(searchText)) ||
                (d.Code != null && d.Code.ToLower().Contains(searchText)) ||
                (d.Teacher != null && d.Teacher.ToLower().Contains(searchText))
            ).ToList();
        }
    }
}