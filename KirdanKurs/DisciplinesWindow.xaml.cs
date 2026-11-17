using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AttendanceApp
{
    public partial class DisciplinesWindow : Window
    {
        private List<Discipline> _allDisciplines;

        public DisciplinesWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _allDisciplines = new List<Discipline>
            {
                new Discipline { Code = "ОП.01", Name = "Экономика организации", Category = "Общепрофессиональные", Teacher = "Иванова А.П.", Hours = 120 },
                new Discipline { Code = "ОП.02", Name = "Статистика", Category = "Общепрофессиональные", Teacher = "Петров С.И.", Hours = 90 },
                new Discipline { Code = "ОП.03", Name = "Менеджмент", Category = "Общепрофессиональные", Teacher = "Сидорова Е.В.", Hours = 108 },
                new Discipline { Code = "МДК.01.01", Name = "Разработка программных модулей", Category = "МДК", Teacher = "Козлов Д.А.", Hours = 250 },
                new Discipline { Code = "МДК.01.02", Name = "Системное программирование", Category = "МДК", Teacher = "Смирнов В.В.", Hours = 180 },
                new Discipline { Code = "МДК.03.01", Name = "Разработка мобильных приложений", Category = "МДК", Teacher = "Федоров А.А.", Hours = 160 },
                new Discipline { Code = "ЕН.01", Name = "Математика", Category = "Общеобразовательные", Teacher = "Николаева О.М.", Hours = 150 },
                new Discipline { Code = "ОГСЭ.01", Name = "Основы философии", Category = "Общеобразовательные", Teacher = "Михайлова Т.Н.", Hours = 48 },
            };

            UpdateGrid();
        }

        private void UpdateGrid()
        {
            DisciplinesDataGrid.ItemsSource = null;
            DisciplinesDataGrid.ItemsSource = _allDisciplines;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new Discipline { Code = "Новый код", Name = "Название", Category = "МДК", Teacher = "ФИО", Hours = 0 };
            _allDisciplines.Add(newItem);
            UpdateGrid();

            DisciplinesDataGrid.SelectedItem = newItem;
            DisciplinesDataGrid.ScrollIntoView(newItem);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            DisciplinesDataGrid.CommitEdit();
            DisciplinesDataGrid.CommitEdit();

            MessageBox.Show("Изменения сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
                if (MessageBox.Show($"Удалить дисциплину \"{item.Name}\"?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _allDisciplines.Remove(item);
                    UpdateGrid();
                }
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            string category = (sender as Button).Tag.ToString();
            if (category == "Все")
            {
                DisciplinesDataGrid.ItemsSource = _allDisciplines;
            }
            else
            {
                var filtered = _allDisciplines.Where(d => d.Category == category).ToList();
                DisciplinesDataGrid.ItemsSource = filtered;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                DisciplinesDataGrid.ItemsSource = _allDisciplines;
                return;
            }
            var searched = _allDisciplines.Where(d =>
                (d.Name != null && d.Name.ToLower().Contains(searchText)) ||
                (d.Code != null && d.Code.ToLower().Contains(searchText)) ||
                (d.Teacher != null && d.Teacher.ToLower().Contains(searchText))
            ).ToList();
            DisciplinesDataGrid.ItemsSource = searched;
        }
    }

    public class Discipline
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Teacher { get; set; }
        public int Hours { get; set; }
    }
}