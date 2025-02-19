using Business.Factories;
using Business.Interfaces;
using Business.Services;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using Presentation.MobileApp.ViewModels;
using System;

namespace Presentation.MobileApp
{
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

            //builder.Services.AddDbContext<AppDbContext>(options =>
            //    options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Initial Catalog=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projects\DataStorage_Assignment\Data\Databases\local_database.mdf;Integrated Security=True");
            builder.Services.AddScoped<IContactPersonService, ContactPersonService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<ITimeFrameService, TimeframeService>();
            //builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<ICompanyRoleRepository, CompanyRoleRepository>();
            builder.Services.AddScoped<ICompanyServiceRepository,  CompanyServiceRepository>();
            builder.Services.AddScoped<IContactPersonRepository, ContactPersonRepository>();
            builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            builder.Services.AddScoped<ICustomerReferenceRepository, CustomerReferenceRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IProjectStatusRepository, ProjectStatusRepository>();
            builder.Services.AddScoped<ITimeframeRepository, TimeframeRepository>();
            builder.Services.AddScoped<IUnitRepository, UnitRepository>();
            builder.Services.AddTransient<Pages.ProjectListPage>();
            builder.Services.AddTransient<Pages.ProjectAddPage>();
            builder.Services.AddTransient<ProjectAddViewModel>();
            builder.Services.AddTransient<ProjectListViewModel>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MainPage>();
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
}
