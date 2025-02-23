using Presentation.MobileApp.ViewModels;

namespace Presentation.MobileApp.Pages;

public partial class ServiceDetailsPage : ContentPage
{
	public ServiceDetailsPage(ServiceDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}