using Business.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Presentation.MobileApp.ApiServices;
using Presentation.MobileApp.ViewModels;

namespace Presentation.MobileApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if WINDOWS
        builder.ConfigureLifecycleEvents(events =>
        {
            // Method pasted from learn.Microsoft.com to center the window at creation
            events.AddWindows(windowsLifecycleBuilder =>
            {
                windowsLifecycleBuilder.OnWindowCreated(window =>
                {
                    window.ExtendsContentIntoTitleBar = false;
                    var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                    var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
                    var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);

                    if (appWindow is not null)
                    {
                        Microsoft.UI.Windowing.DisplayArea displayArea = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(id, Microsoft.UI.Windowing.DisplayAreaFallback.Nearest);
                        if (displayArea is not null)
                        {
                            var CenteredPosition = appWindow.Position;
                            CenteredPosition.X = ((displayArea.WorkArea.Width - appWindow.Size.Width) / 2);
                            CenteredPosition.Y = ((displayArea.WorkArea.Height - appWindow.Size.Height) / 2);
                            appWindow.Move(CenteredPosition);
                        }
                    }
                });
            });
        });
#endif

        builder.Services.AddSingleton<HttpClient>(new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7299/")  // Use the HTTPS URL Claude AI
        });

        builder.Services.AddTransient<ICompanyServiceService, CompanyServiceApiService>();
        builder.Services.AddTransient<IContactPersonService, ContactPersonApiService>();
        builder.Services.AddTransient<ICustomerService, CustomerApiService>();
        builder.Services.AddTransient<IEmployeeService, EmployeeApiService>();
        builder.Services.AddTransient<IProjectService, ProjectApiService>();
        builder.Services.AddTransient<ITimeframeService, TimeframeApiService>();
        builder.Services.AddTransient<ICompanyRoleService, CompanyRoleApiService>();
        builder.Services.AddTransient<IUnitService, UnitApiService>();
        builder.Services.AddTransient<ICurrencyService, CurrencyApiService>();

        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<ProjectAddViewModel>();
        builder.Services.AddTransient<ProjectListViewModel>();
        builder.Services.AddTransient<CustomerAddViewModel>();
        builder.Services.AddTransient<CustomerListViewModel>();
        builder.Services.AddTransient<EmployeeAddViewModel>();
        builder.Services.AddTransient<EmployeeListViewModel>();
        builder.Services.AddTransient<ServiceListViewModel>();
        builder.Services.AddTransient<ServiceAddViewModel>();

        builder.Services.AddTransient<Pages.MainPage>();
        builder.Services.AddTransient<Pages.ProjectListPage>();
        builder.Services.AddTransient<Pages.ProjectAddPage>();
        builder.Services.AddTransient<Pages.CustomerAddPage>();
        builder.Services.AddTransient<Pages.CustomerListPage>();
        builder.Services.AddTransient<Pages.EmployeeAddPage>();
        builder.Services.AddTransient<Pages.EmployeeListPage>();
        builder.Services.AddTransient<Pages.ServiceAddPage>();
        builder.Services.AddTransient<Pages.ServiceListPage>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
