using Presentation.MobileApp.ViewModels;

namespace Presentation.MobileApp.Pages;

public partial class ServiceListPage : ContentPage
{
	public ServiceListPage(ServiceListViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}