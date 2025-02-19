using Presentation.MobileApp.ViewModels;

namespace Presentation.MobileApp.Pages;

public partial class ServiceAddPage : ContentPage
{
	public ServiceAddPage(ServiceAddViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}