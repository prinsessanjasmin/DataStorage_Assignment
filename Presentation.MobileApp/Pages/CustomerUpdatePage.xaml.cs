using Presentation.MobileApp.ViewModels;

namespace Presentation.MobileApp.Pages;

public partial class CustomerUpdatePage : ContentPage
{
	public CustomerUpdatePage(CustomerUpdateViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}