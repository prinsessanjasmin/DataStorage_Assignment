using Presentation.MobileApp.ViewModels;

namespace Presentation.MobileApp.Pages;

public partial class CustomerListPage : ContentPage
{
	public CustomerListPage(CustomerListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((CustomerListViewModel)BindingContext).LoadCustomers();
    }
}