using Presentation.MobileApp.ViewModels;

namespace Presentation.MobileApp.Pages;

public partial class EmployeeUpdatePage : ContentPage
{
	public EmployeeUpdatePage(EmployeeUpdateViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}