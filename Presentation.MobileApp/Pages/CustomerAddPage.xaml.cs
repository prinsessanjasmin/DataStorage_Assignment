using Presentation.MobileApp.ViewModels;

namespace Presentation.MobileApp.Pages;

public partial class CustomerAddPage : ContentPage
{
	public CustomerAddPage(CustomerAddViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}