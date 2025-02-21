using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using Business.Models;
using System.Collections.ObjectModel;

namespace Presentation.MobileApp.ViewModels;

public partial class EmployeeAddViewModel : ObservableObject
{
    private readonly IEmployeeService _employeeApiService;
    private readonly ICompanyRoleService _companyRoleApiService;

    [ObservableProperty]
    private EmployeeModel _employee;

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
    private CompanyRoleEntity _selectedRole;


    public EmployeeAddViewModel(IEmployeeService employeeApiService, ICompanyRoleService companyRoleApiService)
    {
        _employeeApiService = employeeApiService;
        _companyRoleApiService = companyRoleApiService;
        _employee = new EmployeeModel();
        _roles = new ObservableCollection<CompanyRoleEntity>();

        LoadCompanyRoles().ConfigureAwait(false);
    }

    [RelayCommand]
    public async Task SaveChanges()
    {

        if (string.IsNullOrWhiteSpace(FirstName) ||
        string.IsNullOrWhiteSpace(LastName) ||
        string.IsNullOrWhiteSpace(Email) ||
        string.IsNullOrWhiteSpace(PhoneNumber) ||
        SelectedRole == null)
        {
            ErrorMessage = "All fields must be filled out.";
            return;
        }
        
        Employee.FirstName = FirstName;
        Employee.LastName = LastName;
        Employee.Email = Email;
        Employee.PhoneNumber = PhoneNumber;
        Employee.CompanyRoleId = SelectedRole.Id;
        
        var createdEmployee = await _employeeApiService.CreateEmployee(Employee);
        if (createdEmployee != null)
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            SelectedRole = null!; 

            
            ErrorMessage = "Employee created successfully!";
        }
        else
        {
            ErrorMessage = _employeeApiService.ErrorMessage;
        }
    }

    [RelayCommand]
    public async Task LoadCompanyRoles()
    {
        try
        {
            var roleList = await _companyRoleApiService.GetAllCompanyRoles();
            if (roleList != null)
            {
                Roles.Clear();
                foreach (var role in roleList)
                {
                    Roles.Add(role);
                }
                ErrorMessage = _companyRoleApiService.ErrorMessage;
            }
            else
            {
                ErrorMessage = _companyRoleApiService.ErrorMessage;
            }
        }
        catch
        {
            ErrorMessage = _companyRoleApiService.ErrorMessage;
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
