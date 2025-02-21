using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using Presentation.MobileApp.ApiServices;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Presentation.MobileApp.ViewModels;

public partial class ProjectListViewModel : ObservableObject
{
    private readonly IProjectService _projectApiService;

    [ObservableProperty]
    private ObservableCollection<ProjectEntity> _projects;

    [ObservableProperty]
    private string? _errorMessage;

    public ProjectListViewModel(IProjectService projectApiService)
    {
        _projectApiService = projectApiService;
        _projects = new ObservableCollection<ProjectEntity>();

    }

    [RelayCommand]
    public async Task LoadProjects()
    {
        try
        {
            var projectList = await _projectApiService.GetAllProjects();
            if (projectList != null)
            {
                Projects.Clear(); 
                foreach (var project in projectList)
                {
                    Projects.Add(project);
                    Debug.WriteLine($"Added: {project.Title}");
                }
                ErrorMessage = _projectApiService.ErrorMessage;
            }
            else
            {
                Debug.WriteLine("projectlist was null");
                ErrorMessage = _projectApiService.ErrorMessage;
            }
        }
        catch 
        {
            Debug.WriteLine($"Exception: {_projectApiService.ErrorMessage}");  // Debug output
            ErrorMessage = _projectApiService.ErrorMessage;
        }
    }

    [RelayCommand]
    public async Task NavigateToProjectAdd()
    {
        await Shell.Current.GoToAsync("ProjectAddPage");
    }

    [RelayCommand]
    public async Task NavigateToHome()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}
