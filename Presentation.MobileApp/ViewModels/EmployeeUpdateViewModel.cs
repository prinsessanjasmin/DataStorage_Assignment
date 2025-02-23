using Business.Interfaces;
using Business.Models;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using System.Collections.ObjectModel;
using System.Data;

namespace Presentation.MobileApp.ViewModels;

public partial class EmployeeUpdateViewModel : ObservableObject, IQueryAttributable
{
    private readonly IEmployeeService _employeeApiService;
    private readonly ICompanyRoleService _companyRoleApiService;
    private readonly EmployeeListViewModel _employeeListViewModel;

    [ObservableProperty]
    private EmployeeEntity _employee;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private ObservableCollection<CompanyRoleEntity> _roles;

    [ObservableProperty]
    private string _firstName;

    [ObservableProperty]
    private string _lastName;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _phoneNumber;

    [ObservableProperty]
    private int _companyRoleId;

    [ObservableProperty]
    private CompanyRoleEntity _selectedRole;

    public EmployeeUpdateViewModel(IEmployeeService employeeApiService, ICompanyRoleService companyRoleApiService, EmployeeListViewModel employeeListViewModel)
    {
        _employeeApiService = employeeApiService;
        _companyRoleApiService = companyRoleApiService;
        _employeeListViewModel = employeeListViewModel;
        _roles = new ObservableCollection<CompanyRoleEntity>();
        Employee = new EmployeeEntity();
    }

    [RelayCommand]
    public async Task SaveChanges()
    {
        if (Employee == null || Employee.Id == 0)
        {
            ErrorMessage = "Invalid employee ID. Cannot update.";
            return;
        }

        var updatedEmployee = new EmployeeEntity
        {
            Id = Employee.Id,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            PhoneNumber = PhoneNumber,
            CompanyRoleId = SelectedRole.Id,
            CompanyRole = SelectedRole
        };
       
        var finishedEmployee = await _employeeApiService.UpdateEmployee(Employee.Id, updatedEmployee);
        if (finishedEmployee != null)
        {
            await Shell.Current.GoToAsync("EmployeeListPage");
            ErrorMessage = "Employee updated successfully!";
        }
        else
        {
            ErrorMessage = _employeeApiService.ErrorMessage;
        }
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        //Method constructed by ChatGPT 4o

        if (query.TryGetValue("employeeId", out var idValue) && int.TryParse(idValue.ToString(), out int employeeId))
        {
            if (Employee == null)
            {
                Employee = new EmployeeEntity();
            }
            Employee.Id = employeeId;
            await LoadEmployeeDetails();
        }
    }

    private async Task LoadEmployeeDetails()
    {
        var employee = await _employeeApiService.GetEmployeeById(Employee.Id);
        if (employee != null)
        {
            Employee = employee;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Email = employee.Email;
            PhoneNumber = employee.PhoneNumber;
            
            Roles = new ObservableCollection<CompanyRoleEntity>(await _companyRoleApiService.GetAllCompanyRoles());

            SelectedRole = Roles.FirstOrDefault(x => x.Id == employee.CompanyRole.Id);
        }
        else
        {
            ErrorMessage = "Error loading employee details.";
        }
    }

    [RelayCommand]
    public async Task DeleteEmployee(int id)
    {
        bool result = await _employeeApiService.DeleteEmployee(Employee.Id);
        if (result)
        {
            await _employeeListViewModel.LoadEmployees();
            await Shell.Current.GoToAsync("EmployeeListPage");
            ErrorMessage = "Employee successfully deleted.";
        }
        else
        {
            ErrorMessage = _employeeApiService.ErrorMessage;
        }
    }

    [RelayCommand]
    public async Task NavigateToEmployeeList()
    {
        await Shell.Current.GoToAsync("EmployeeListPage");
    }

    [RelayCommand]
    public async Task NavigateToHome()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}
