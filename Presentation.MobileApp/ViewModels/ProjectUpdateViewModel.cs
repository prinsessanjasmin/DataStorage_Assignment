﻿using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using System.Collections.ObjectModel;
using System.Data;

namespace Presentation.MobileApp.ViewModels;

public partial class ProjectUpdateViewModel : ObservableObject, IQueryAttributable
{
    private readonly IProjectService _projectApiService;
    
    private readonly ICompanyServiceService _companyServiceApiService;
    
    private readonly IEmployeeService _employeeApiService;
    
    private readonly ITimeframeService _timeframeApiService;
    
    private readonly IProjectStatusService _projectStatusApiService;
    
    private readonly ICustomerService _customerApiService;

    private readonly ProjectListViewModel _projectListViewModel;

    [ObservableProperty]
    private ProjectEntity _project;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private ObservableCollection<ProjectEntity> _projects;

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string? _about;

    [ObservableProperty]
    private int _quantity;

    [ObservableProperty]
    private decimal _totalPrice;

    [ObservableProperty]
    private DateTime _selectedStartDate;

    [ObservableProperty]
    private DateTime _selectedEndDate;

    [ObservableProperty]
    private CompanyServiceEntity _selectedCompanyService;

    [ObservableProperty]
    private EmployeeEntity _selectedEmployee;

    [ObservableProperty]
    private CustomerEntity _selectedCustomer;

    [ObservableProperty]
    private ProjectStatusEntity _selectedProjectStatus;

    [ObservableProperty]
    private ObservableCollection<TimeframeEntity> _timeframes;

    [ObservableProperty]
    private ObservableCollection<ProjectStatusEntity> _projectStatuses;

    [ObservableProperty]
    private ObservableCollection<CustomerEntity> _customers;

    [ObservableProperty]
    private ObservableCollection<EmployeeEntity> _employees;

    [ObservableProperty]
    private ObservableCollection<CompanyServiceEntity> _companyServices;


    public ProjectUpdateViewModel(IProjectService projectApiService, IEmployeeService employeeApiService, ICompanyServiceService companyServiceApiService, ICustomerService customerApiService, ITimeframeService timeframeApiService, IProjectStatusService projectStatusApiService, ProjectListViewModel projectListViewModel)
    {
        _projectApiService = projectApiService;
        _employeeApiService = employeeApiService;
        _companyServiceApiService = companyServiceApiService;
        _customerApiService = customerApiService;
        _timeframeApiService = timeframeApiService;
        _projectStatusApiService = projectStatusApiService;
        _projectListViewModel = projectListViewModel;
        _project = new ProjectEntity();

        _companyServices = new ObservableCollection<CompanyServiceEntity>();
        _employees = new ObservableCollection<EmployeeEntity>();
        _customers = new ObservableCollection<CustomerEntity>();
        _projectStatuses = new ObservableCollection<ProjectStatusEntity>();
        _timeframes = new ObservableCollection<TimeframeEntity>();
    }

    [RelayCommand]
    public async Task SaveChanges()
    {
        Project.Title = Title;
        Project.About = About;
        Project.Quantity = Quantity;
        Project.TotalPrice = Project.TotalPrice;
        
        if (Project.Timeframe == null)
        {
            Project.Timeframe = new TimeframeEntity();
        }

        Project.Timeframe.StartDate = SelectedStartDate;
        Project.Timeframe.EndDate = SelectedEndDate;
        
        Project.ProjectStatusId = SelectedProjectStatus.Id;
        Project.ProjectStatus = SelectedProjectStatus;

        Project.CustomerId = SelectedCustomer.Id;
        Project.Customer = SelectedCustomer;

        Project.ProjectManagerId = SelectedEmployee.Id;
        Project.ProjectManager = SelectedEmployee;

        Project.CompanyServiceId = SelectedCompanyService.Id;
        Project.CompanyService = SelectedCompanyService;

        var finishedProject = await _projectApiService.UpdateProject(Project.Id, Project);
        if (finishedProject != null)
        {
            await Shell.Current.GoToAsync("ProjectListPage");
            ErrorMessage = "Project updated successfully!";
        }
        else
        {
            ErrorMessage = _projectApiService.ErrorMessage;
        }
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        //Method constructed by ChatGPT 4o

        if (query.TryGetValue("projectId", out var idValue) && int.TryParse(idValue.ToString(), out int projectId))
        {
            Project.Id = projectId;
            await LoadProjectDetails();
        }
    }

    private async Task LoadProjectDetails()
    {
        var project = await _projectApiService.GetProjectById(Project.Id);
        if (project != null)
        {
            Title = project.Title;
            About = project.About;
            Quantity = project.Quantity;
            
            if (project.Timeframe != null)
            {
                SelectedStartDate = project.Timeframe.StartDate;
                SelectedEndDate = project.Timeframe.EndDate;
            }
            else
            {
                SelectedStartDate = DateTime.Today; 
                SelectedEndDate = DateTime.Today.AddDays(14); 
            }

            ProjectStatuses = new ObservableCollection<ProjectStatusEntity>(await _projectStatusApiService.GetAllProjectStatuses());
            SelectedProjectStatus = ProjectStatuses.FirstOrDefault(x => x.Id == project.ProjectStatus.Id);
            
            Customers = new ObservableCollection<CustomerEntity>(await _customerApiService.GetAllCustomers());
            SelectedCustomer = Customers.FirstOrDefault(x => x.Id == project.Customer.Id);

            Employees = new ObservableCollection<EmployeeEntity>(await _employeeApiService.GetAllEmployees());
            SelectedEmployee = Employees.FirstOrDefault(x => x.Id == project.ProjectManager.Id);

            CompanyServices = new ObservableCollection<CompanyServiceEntity>(await _companyServiceApiService.GetAllCompanyServices());
            SelectedCompanyService = CompanyServices.FirstOrDefault(x => x.Id == project.CompanyService.Id);
       
        }
        else
        {
            ErrorMessage = "Error loading project details.";
        }
    }

    [RelayCommand]
    public async Task DeleteProject(int id)
    {
        bool result = await _projectApiService.DeleteProject(Project.Id);
        if (result)
        {
            await _projectListViewModel.LoadProjects();
            await Shell.Current.GoToAsync("ProjectListPage");
            ErrorMessage = "Project successfully deleted.";
        }
        else
        {
            ErrorMessage = _projectApiService.ErrorMessage;
        }
    }

    [RelayCommand]
    public async Task NavigateToHome()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}
