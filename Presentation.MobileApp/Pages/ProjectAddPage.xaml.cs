using Presentation.MobileApp.ViewModels;
using CommunityToolkit.Mvvm.Input;

namespace Presentation.MobileApp.Pages;

public partial class ProjectAddPage : ContentPage
{
    public ProjectAddPage(ProjectAddViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
