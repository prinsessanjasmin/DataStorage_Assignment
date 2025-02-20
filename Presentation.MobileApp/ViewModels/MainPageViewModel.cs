﻿
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Presentation.MobileApp.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private string _errorMessage;
    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    [RelayCommand]
    public async Task NavigateToProjectList()
    {
        await Shell.Current.GoToAsync("ProjectListPage");
    }

    [RelayCommand]
    public async Task NavigateToCustomersList()
    {
        await Shell.Current.GoToAsync("CustomerListPage");
    }

    [RelayCommand]
    public async Task NavigateToServiceList()
    {
        await Shell.Current.GoToAsync("ServiceListPage");
    }

    [RelayCommand]
    public async Task NavigateToEmployeeList()
    {
        await Shell.Current.GoToAsync("EmployeeListPage");
    }
}
