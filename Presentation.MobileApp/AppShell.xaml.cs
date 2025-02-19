

using Presentation.MobileApp.Pages;

namespace Presentation.MobileApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ProjectListPage), typeof(ProjectListPage));
            Routing.RegisterRoute(nameof(ProjectAddPage), typeof(ProjectAddPage));
        }
    }
}
