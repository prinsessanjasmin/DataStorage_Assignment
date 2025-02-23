using CommunityToolkit.Mvvm.ComponentModel;
using Business.Interfaces;
using Data.Entities;
using CommunityToolkit.Mvvm.Input;


namespace Presentation.MobileApp.ViewModels;

public partial class ServiceDetailsViewModel : ObservableObject, IQueryAttributable
{
    private readonly ICompanyServiceService _companyServiceApiService;
    private readonly ServiceListViewModel _serviceListViewModel;

    [ObservableProperty]
    private CompanyServiceEntity _companyService;

    [ObservableProperty]
    private string? _errorMessage;

    public ServiceDetailsViewModel(ICompanyServiceService companyServiceApiService, ServiceListViewModel serviceListViewModel )
    {
        _companyServiceApiService = companyServiceApiService;
        _serviceListViewModel = serviceListViewModel;        
        CompanyService = new CompanyServiceEntity();
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Console.WriteLine("ApplyQueryAttributes called!");
        Console.WriteLine("Received query parameters:");
        foreach (var key in query.Keys)
        {
            Console.WriteLine($"KEYS {key}: {query[key]}");
        }

        //Method constructed by ChatGPT 4o
        if (query.TryGetValue("serviceId", out var idValue) && int.TryParse(idValue.ToString(), out int serviceId))
        {
            CompanyService = await _companyServiceApiService.GetCompanyServiceById(serviceId);
        }
    }

    [RelayCommand]
    public async Task NavigateToServiceUpdate(int id)
    {
        if (CompanyService != null)
        {
            Console.WriteLine($"Navigating to ServiceUpdatePage with ID: {CompanyService.Id}");
            await Shell.Current.GoToAsync("ServiceUpdatePage", new Dictionary<string, object>
            {
                { "companyServiceId", CompanyService.Id }
            });
        }
        else
        {
            ErrorMessage = "Error: No service selected.";
            Console.WriteLine("Error: No service selected.");
        }
    }

    [RelayCommand]
    public async Task DeleteService(int id)
    {
        bool result = await _companyServiceApiService.DeleteCompanyService(id);
        if (result)
        {
            await _serviceListViewModel.LoadCompanyServices();
            ErrorMessage = "Service successfully deleted.";
        }
        else
        {
            ErrorMessage = _companyServiceApiService.ErrorMessage;
        }
    }

    [RelayCommand]
    public async Task NavigateToHome()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}
