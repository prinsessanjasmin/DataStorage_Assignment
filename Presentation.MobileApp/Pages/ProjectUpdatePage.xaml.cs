using Presentation.MobileApp.ViewModels;

namespace Presentation.MobileApp.Pages;

public partial class ProjectUpdatePage : ContentPage
{
	public ProjectUpdatePage(ProjectUpdateViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}