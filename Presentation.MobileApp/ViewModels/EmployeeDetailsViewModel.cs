using Business.Interfaces;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using System.Collections.ObjectModel;

namespace Presentation.MobileApp.ViewModels;

public partial class EmployeeDetailsViewModel : ObservableObject, IQueryAttributable
{
    private readonly IEmployeeService _employeeApiService;
    private readonly EmployeeListViewModel _employeeListViewModel;

    [ObservableProperty]
    private EmployeeEntity _employee;

    [ObservableProperty]
    private string? _errorMessage; 

    public EmployeeDetailsViewModel(IEmployeeService employeeApiService, EmployeeListViewModel employeeListViewModel)
    {
        _employeeApiService = employeeApiService;
        _employeeListViewModel = employeeListViewModel;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        //Method constructed by ChatGPT 4o
        if (query.TryGetValue("employeeId", out var idValue) && int.TryParse(idValue.ToString(), out int employeeId))
        {
            Employee = await _employeeApiService.GetEmployeeById(employeeId);
        }
    }

    [RelayCommand]
    public async Task NavigateToEmployeeUpdate(int id)
    {
        await Shell.Current.GoToAsync($"EmployeeUpdatePage?employeeId={id}");
    }

    [RelayCommand]
    public async Task DeleteEmployee(int id)
    {
        bool result = await _employeeApiService.DeleteEmployee(id);
        if (result)
        {
            await _employeeListViewModel.LoadEmployees();
            ErrorMessage = "Employee successfully deleted.";
        }
        else
        {
            ErrorMessage = _employeeApiService.ErrorMessage;
        }
    }

    [RelayCommand]
    public async Task NavigateToHome()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}
