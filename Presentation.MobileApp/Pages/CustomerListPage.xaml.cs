using Presentation.MobileApp.ViewModels;
using System.Diagnostics;

namespace Presentation.MobileApp.Pages;

public partial class CustomerListPage : ContentPage
{
	public CustomerListPage(CustomerListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}