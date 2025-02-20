
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using Presentation.MobileApp.ApiServices;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Presentation.MobileApp.ViewModels;

public partial class CustomerListViewModel : ObservableObject
{
    private readonly ICustomerService _customerApiService;

    [ObservableProperty]
    private ObservableCollection<CustomerEntity> _customers;

    [ObservableProperty]
    private string _errorMessage;

    public CustomerListViewModel(ICustomerService customerApiService)
    {
        _customerApiService = customerApiService;
        _customers = new ObservableCollection<CustomerEntity>();
    }

    [RelayCommand]
    public async Task LoadCustomers()
    {
        try
        {
            var customerList = await _customerApiService.GetAllCustomers();
            if (customerList != null)
            {
                Customers.Clear();
                foreach (var customer in customerList)
                {
                    Customers.Add(customer);
                }
                ErrorMessage = _customerApiService.ErrorMessage;
            }
            else
            {
                ErrorMessage = _customerApiService.ErrorMessage;
            }
        }
        catch
        {
            ErrorMessage = _customerApiService.ErrorMessage;
        }
    }

    [RelayCommand]
    public async Task NavigateToCustomerAdd()
    {
        await Shell.Current.GoToAsync("CustomerAddPage");
    }

    [RelayCommand]
    public async Task NavigateToHome()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}
