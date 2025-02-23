using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using Presentation.MobileApp.ApiServices;
using System.Collections.ObjectModel;

namespace Presentation.MobileApp.ViewModels
{
    public partial class EmployeeListViewModel : ObservableObject
    {
        private readonly IEmployeeService _employeeApiService;

        [ObservableProperty]
        private ObservableCollection<EmployeeEntity> _employees;

        [ObservableProperty]
        private string? _errorMessage;

        public EmployeeListViewModel(IEmployeeService employeeApiService)
        {
            _employeeApiService = employeeApiService;
            _employees = new ObservableCollection<EmployeeEntity>();

            LoadEmployees().ConfigureAwait(false);
        }

        [RelayCommand]
        public async Task LoadEmployees()
        {
            try
            {
                var employeeList = await _employeeApiService.GetAllEmployees();
                if (employeeList != null)
                {
                    Employees.Clear();
                    foreach (var employee in employeeList)
                    {
                        Employees.Add(employee);
                    }
                    ErrorMessage = _employeeApiService.ErrorMessage;
                }
                else
                {
                    ErrorMessage = _employeeApiService.ErrorMessage;
                }
            }
            catch
            {
                ErrorMessage = _employeeApiService.ErrorMessage;
            }
        }

        [RelayCommand]
        public async Task NavigateToEmployeeDetails(int id)
        {
            await Shell.Current.GoToAsync($"EmployeeDetailsPage?employeeId={id}");
        }

        [RelayCommand]
        public async Task NavigateToEmployeeAdd()
        {
            await Shell.Current.GoToAsync("EmployeeAddPage");
        }

        [RelayCommand]
        public async Task NavigateToHome()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
