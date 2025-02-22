
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Data;

namespace Presentation.MobileApp.ViewModels;

public partial class CustomerAddViewModel : ObservableObject
{
    private readonly ICustomerService _customerApiService;

    [ObservableProperty]
    private CustomerModel _customer;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private string? _customerName;

    public CustomerAddViewModel(ICustomerService customerApiService, CustomerListViewModel customerListViewModel)
    {
        _customerApiService = customerApiService;
        _customer = new CustomerModel();
 
    }

    [RelayCommand]
    public async Task SaveChanges()
    {

        if (string.IsNullOrWhiteSpace(CustomerName))
        {
            ErrorMessage = "The customer name field must be filled out.";
            return;
        }

        Customer.CustomerName = CustomerName;
        

        var createdCustomer = await _customerApiService.CreateCustomer(Customer);
        if (createdCustomer != null)
        {
            CustomerName = string.Empty;

            ErrorMessage = "Customer created successfully!";
        }
        else
        {
            ErrorMessage = _customerApiService.ErrorMessage;
        }
    }

    [RelayCommand]
    public async Task NavigateToCustomerList()
    {
        await Shell.Current.GoToAsync("CustomerListPage");
    }

    [RelayCommand]
    public async Task NavigateToHome()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}
