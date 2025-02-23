using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;

namespace Presentation.MobileApp.ViewModels;

public partial class ProjectDetailsViewModel : ObservableObject, IQueryAttributable
{
    private readonly IProjectService _projectApiService;
    private readonly ProjectListViewModel _projectListViewModel;

    [ObservableProperty]
    private ProjectEntity _project;

    [ObservableProperty]
    private string? _errorMessage;

    public ProjectDetailsViewModel(IProjectService projectApiService, ProjectListViewModel projectListViewModel)
    {
        _projectApiService = projectApiService;
        _projectListViewModel = projectListViewModel;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        //Method constructed by ChatGPT 4o
        if (query.TryGetValue("projectId", out var idValue) && int.TryParse(idValue.ToString(), out int projectId))
        {
            Project = await _projectApiService.GetProjectById(projectId);
        }
    }

    [RelayCommand]
    public async Task NavigateToProjectUpdate(int id)
    {
        await Shell.Current.GoToAsync($"ProjectUpdatePage?projectId={id}");
    }

    [RelayCommand]
    public async Task DeleteProject(int id)
    {
        bool result = await _projectApiService.DeleteProject(id);
        if (result)
        {
            await _projectListViewModel.LoadProjects();
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
