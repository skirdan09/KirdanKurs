using System.Windows;

namespace AttendanceApp
{
    public partial class App : Application
    {
        public static string CurrentUser { get; set; } = "Преподаватель";
        public static string CurrentRole { get; set; } = "преподаватель";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Database.Initialize(); 
        }
    }
}