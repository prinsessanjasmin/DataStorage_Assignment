using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Presentation.MobileApp.ViewModels;

public partial class ProjectListViewModel : ObservableObject
{
    [RelayCommand]
    public async Task NavigateToProjectAdd()
    {
        await Shell.Current.GoToAsync("ProjectAddPage");
    }

    [RelayCommand]
    public async Task NavigateToHome()
    {
        await Shell.Current.GoToAsync("MainPage");
    }
}
