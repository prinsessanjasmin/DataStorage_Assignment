using Presentation.MobileApp.ViewModels;

namespace Presentation.MobileApp.Pages;

public partial class EmployeeDetailsPage : ContentPage
{
	public EmployeeDetailsPage(EmployeeDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}