using Presentation.MobileApp.ViewModels;

namespace Presentation.MobileApp.Pages;

public partial class ServiceListPage : ContentPage
{
	public ServiceListPage(ServiceListViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((ServiceListViewModel)BindingContext).LoadCompanyServices();
    }
}