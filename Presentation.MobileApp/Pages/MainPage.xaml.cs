using Presentation.MobileApp.ViewModels;
using System.Collections.ObjectModel;

namespace Presentation.MobileApp;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
