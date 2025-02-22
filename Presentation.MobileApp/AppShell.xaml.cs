using Presentation.MobileApp.Pages;

namespace Presentation.MobileApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(ProjectListPage), typeof(ProjectListPage));
            Routing.RegisterRoute(nameof(ProjectAddPage), typeof(ProjectAddPage));
            Routing.RegisterRoute(nameof(ProjectDetailsPage), typeof(ProjectDetailsPage));
            Routing.RegisterRoute(nameof(ProjectUpdatePage), typeof(ProjectUpdatePage));

            Routing.RegisterRoute(nameof(CustomerListPage), typeof(CustomerListPage));
            Routing.RegisterRoute(nameof(CustomerAddPage), typeof(CustomerAddPage));
            Routing.RegisterRoute(nameof(CustomerUpdatePage), typeof(CustomerUpdatePage));

            Routing.RegisterRoute(nameof(EmployeeListPage), typeof(EmployeeListPage));
            Routing.RegisterRoute(nameof(EmployeeAddPage), typeof(EmployeeAddPage));
            Routing.RegisterRoute(nameof(EmployeeDetailsPage), typeof(EmployeeDetailsPage));
            Routing.RegisterRoute(nameof(EmployeeUpdatePage), typeof(EmployeeUpdatePage));

            Routing.RegisterRoute(nameof(ServiceListPage), typeof(ServiceListPage));
            Routing.RegisterRoute(nameof(ServiceAddPage), typeof(ServiceAddPage));
            Routing.RegisterRoute(nameof(ServiceDetailsPage), typeof(ServiceDetailsPage));
            Routing.RegisterRoute(nameof(ServiceUpdatePage), typeof(ServiceUpdatePage));
        }
    }
}
