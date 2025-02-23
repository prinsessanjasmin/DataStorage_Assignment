using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using System.Collections.ObjectModel;
using System.Data;

namespace Presentation.MobileApp.ViewModels;

public partial class CustomerUpdateViewModel : ObservableObject, IQueryAttributable
{

    private readonly ICustomerService _customerApiService;
    private readonly CustomerListViewModel _customerListViewModel;

    [ObservableProperty]
    private CustomerEntity _customer;

    [ObservableProperty]
    private ObservableCollection<CustomerEntity> _customers;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private string _customerName;


    public CustomerUpdateViewModel(ICustomerService customerApiService, CustomerListViewModel customerListViewModel)
    {
        _customerApiService = customerApiService;
        _customerListViewModel = customerListViewModel;
        _customers = new ObservableCollection<CustomerEntity>();
        _customer = new CustomerEntity();
    }

    [RelayCommand]
    public async Task SaveChanges()
    {
        Customer.CustomerName = CustomerName;

        var finishedCustomer = await _customerApiService.UpdateCustomer(Customer.Id, Customer);
        if (finishedCustomer != null)
        {
            await Shell.Current.GoToAsync("CustomerListPage");
            ErrorMessage = "Customer updated successfully!";
        }
        else
        {
            ErrorMessage = _customerApiService.ErrorMessage;
        }
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        //Method constructed by ChatGPT 4o

        if (query.TryGetValue("customerId", out var idValue) && int.TryParse(idValue.ToString(), out int customerId))
        {
            Customer.Id = customerId;
            await LoadCustomerDetails();
        }
    }

    private async Task LoadCustomerDetails()
    {
        var customer = await _customerApiService.GetCustomerById(Customer.Id);
        if (customer != null)
        {
            CustomerName = customer.CustomerName;
            
        }
        else
        {
            ErrorMessage = "Error loading customer details.";
        }
    }

    [RelayCommand]
    public async Task DeleteCustomer(int id)
    {
        bool result = await _customerApiService.DeleteCustomer(Customer.Id);
        if (result)
        {
            await _customerListViewModel.LoadCustomers();
            await Shell.Current.GoToAsync("CustomerListPage");
            ErrorMessage = "Customer successfully deleted.";
        }
        else
        {
            ErrorMessage = _customerApiService.ErrorMessage;
        }
    }

    [RelayCommand]
    public async Task NavigateToHome()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

}
