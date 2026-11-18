using System.Windows;

namespace AttendanceApp
{
    public partial class GroupsWindow : Window
    {
        public GroupsWindow()
        {
            InitializeComponent();
            GroupsDataGrid.ItemsSource = Database.Groups;
        }
    }
}