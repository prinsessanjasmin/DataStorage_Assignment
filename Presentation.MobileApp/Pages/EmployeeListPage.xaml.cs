using Presentation.MobileApp.ViewModels;

namespace Presentation.MobileApp.Pages;

public partial class EmployeeListPage : ContentPage
{
	public EmployeeListPage(EmployeeListViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}