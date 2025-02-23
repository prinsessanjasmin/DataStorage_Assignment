using Presentation.MobileApp.ViewModels;

namespace Presentation.MobileApp.Pages;

public partial class ServiceUpdatePage : ContentPage
{
	public ServiceUpdatePage(ServiceUpdateViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}