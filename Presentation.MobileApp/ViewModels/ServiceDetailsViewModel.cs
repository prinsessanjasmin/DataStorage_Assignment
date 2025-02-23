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
    private CompanyServiceEntity _service;

    [ObservableProperty]
    private string? _errorMessage;

    public ServiceDetailsViewModel(ICompanyServiceService companyServiceApiService, ServiceListViewModel serviceListViewModel )
    {
        _companyServiceApiService = companyServiceApiService;
        _serviceListViewModel = serviceListViewModel;        
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        //Method constructed by ChatGPT 4o
        if (query.TryGetValue("serviceId", out var idValue) && int.TryParse(idValue.ToString(), out int serviceId))
        {
            Service = await _companyServiceApiService.GetCompanyServiceById(serviceId);
        }
    }

    [RelayCommand]
    public async Task NavigateToServiceUpdate(int id)
    {
        await Shell.Current.GoToAsync($"ServiceUpdatePage?serviceId={id}");
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
