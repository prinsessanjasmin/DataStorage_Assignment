using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using Data.Interfaces;
using System.Collections.ObjectModel;
using System.Data;

namespace Presentation.MobileApp.ViewModels;

public partial class ProjectAddViewModel : ObservableObject
{
    private readonly IProjectService _projectApiService;
    private readonly IEmployeeService _employeeApiService;
    private readonly ICompanyServiceService _companyServiceApiService;
    private readonly ICustomerService _customerApiService;
    private readonly ITimeframeService _timeframeApiService;
    private readonly IProjectStatusService _projectStatusApiService;


    [ObservableProperty]
    private ProjectModel _project;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private ObservableCollection<CompanyServiceEntity> _companyServices;

    [ObservableProperty]
    private ObservableCollection<EmployeeEntity> _employees;

    [ObservableProperty]
    private ObservableCollection<CustomerEntity> _customers;

    [ObservableProperty]
    private ObservableCollection<ProjectStatusEntity> _projectStatuses;

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _about;

    [ObservableProperty]
    private int _quantity;

    [ObservableProperty]
    private DateTime _startDate = DateTime.Today;

    [ObservableProperty]
    private DateTime _endDate = DateTime.Today;

    [ObservableProperty]
    private CompanyServiceEntity _selectedCompanyService;

    [ObservableProperty]
    private EmployeeEntity _selectedEmployee;

    [ObservableProperty]
    private CustomerEntity _selectedCustomer;

    [ObservableProperty]
    private ProjectStatusEntity _selectedProjectStatus;

    public ProjectAddViewModel(IProjectService projectApiService, IEmployeeService employeeApiService, ICompanyServiceService companyServiceApiService, ICustomerService customerApiService, ITimeframeService timeframeApiService, IProjectStatusService projectStatusApiService)
    {
        _projectApiService = projectApiService;
        _employeeApiService = employeeApiService;
        _companyServiceApiService = companyServiceApiService;
        _customerApiService = customerApiService;
        _timeframeApiService = timeframeApiService;
        _projectStatusApiService = projectStatusApiService;
        _project = new ProjectModel();
        _companyServices = new ObservableCollection<CompanyServiceEntity>();
        _employees = new ObservableCollection<EmployeeEntity>();
        _customers = new ObservableCollection<CustomerEntity>();
        _projectStatuses = new ObservableCollection<ProjectStatusEntity>();

        LoadCompanyServices().ConfigureAwait(false);
        LoadCustomers().ConfigureAwait(false);
        LoadEmployees().ConfigureAwait(false);
        LoadProjectStatuses().ConfigureAwait(false);
    }

    [RelayCommand]
    public async Task SaveChanges()
    {

        if (string.IsNullOrWhiteSpace(Title) ||
        Quantity == 0 ||
        EndDate <= StartDate ||
        SelectedProjectStatus == null ||
        SelectedCustomer == null || 
        SelectedEmployee == null || 
        SelectedCompanyService == null)
        {
            ErrorMessage = "All fields must be filled out.";
            return;
        }

        Project.Title = Title;
        Project.About = About;
        Project.Quantity = Quantity;
        Project.StartDate = StartDate;
        Project.EndDate = EndDate;
        Project.ProjectStatusId = SelectedProjectStatus.Id;
        Project.CustomerId = SelectedCustomer.Id;
        Project.ProjectManagerId = SelectedEmployee.Id;
        Project.CompanyServiceId = SelectedCompanyService.Id;

        var createdProject = await _projectApiService.CreateProject(Project);
        if (createdProject != null)
        {
            Title = string.Empty;
            About = string.Empty;
            Quantity = 0;
            StartDate = default;
            EndDate = default;
            SelectedProjectStatus = null!;
            SelectedCustomer = null!;
            SelectedEmployee = null!;
            SelectedCompanyService = null!;

            ErrorMessage = "Project created successfully!";
        }
        else
        {
            ErrorMessage = _projectApiService.ErrorMessage;
        }
    }

    [RelayCommand]
    public async Task LoadCompanyServices()
    {
        try
        {
            var companyServiceList = await _companyServiceApiService.GetAllCompanyServices();
            if (companyServiceList != null)
            {
                CompanyServices.Clear();
                foreach (var service in companyServiceList)
                {
                    CompanyServices.Add(service);
                }
                ErrorMessage = _companyServiceApiService.ErrorMessage;
            }
            else
            {
                ErrorMessage = _companyServiceApiService.ErrorMessage;
            }
        }
        catch
        {
            ErrorMessage = _companyServiceApiService.ErrorMessage;
        }
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
    public async Task LoadCustomers()
    {
        try
        {
            var customerList = await _customerApiService.GetAllCustomers();
            if (customerList != null)
            {
                Customers.Clear();
                foreach (var customer in customerList)
                {
                    Customers.Add(customer);
                }
                ErrorMessage = _customerApiService.ErrorMessage;
            }
            else
            {
                ErrorMessage = _customerApiService.ErrorMessage;
            }
        }
        catch
        {
            ErrorMessage = _customerApiService.ErrorMessage;
        }
    }

    [RelayCommand]
    public async Task LoadProjectStatuses()
    {
        try
        {
            var projectStatusList = await _projectStatusApiService.GetAllProjectStatuses();
            if (projectStatusList != null)
            {
                ProjectStatuses.Clear();
                foreach (var projectStatus in projectStatusList)
                {
                    ProjectStatuses.Add(projectStatus);
                }
                ErrorMessage = _projectStatusApiService.ErrorMessage;
            }
            else
            {
                ErrorMessage = _projectStatusApiService.ErrorMessage;
            }
        }
        catch
        {
            ErrorMessage = _projectStatusApiService.ErrorMessage;
        }
    }

    [RelayCommand]
    public async Task NavigateToProjectList()
    {
        await Shell.Current.GoToAsync("ProjectListPage");
    }

    [RelayCommand]
    public async Task NavigateToHome()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}
