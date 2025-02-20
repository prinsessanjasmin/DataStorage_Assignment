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

            builder.Services.AddSingleton<HttpClient>(new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7299/")  // Use the HTTPS URL Claude AI
            });

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
}
