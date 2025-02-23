using Presentation.MobileApp.ViewModels;

namespace Presentation.MobileApp.Pages;

public partial class ProjectDetailsPage : ContentPage
{
	public ProjectDetailsPage(ProjectDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}