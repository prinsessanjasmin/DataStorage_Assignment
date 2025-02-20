using CommunityToolkit.Mvvm.Input;
using Presentation.MobileApp.ViewModels;

namespace Presentation.MobileApp.Pages;

public partial class ProjectListPage : ContentPage
{
    public ProjectListPage(ProjectListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((ProjectListViewModel)BindingContext).LoadProjects();
    }
}