using Presentation.MobileApp.ViewModels;

namespace Presentation.MobileApp.Pages;

public partial class EmployeeAddPage : ContentPage
{
	public EmployeeAddPage(EmployeeAddViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}