using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using Presentation.MobileApp.ApiServices;
using System.Collections.ObjectModel;

namespace Presentation.MobileApp.ViewModels;

public partial class ServiceListViewModel : ObservableObject
{
    private readonly ICompanyServiceService _companyServiceApiService;

    [ObservableProperty]
    private ObservableCollection<CompanyServiceEntity> _companyServices;

    [ObservableProperty]
    private string? _errorMessage;

    public ServiceListViewModel(ICompanyServiceService companyServiceApiService)
    {
        _companyServiceApiService = companyServiceApiService;
        _companyServices = new ObservableCollection<CompanyServiceEntity>();
    }

    [RelayCommand]
    public async Task LoadCompanyServices()
    {
        try
        {
            var companyServiceList = await _companyServiceApiService.GetAllCompanyServices();
            if (companyServiceList != null)
            {
                CompanyServices.Clear();
                foreach (var companyService in companyServiceList)
                {
                    CompanyServices.Add(companyService);
                }
                ErrorMessage = _companyServiceApiService.ErrorMessage;
            }
            else
            {
                ErrorMessage = _companyServiceApiService.ErrorMessage;
            }
        }
        catch
        {
            ErrorMessage = _companyServiceApiService.ErrorMessage;
        }
    }

    [RelayCommand]
    public async Task NavigateToServiceAdd()
    {
        await Shell.Current.GoToAsync("ServiceAddPage");
    }

    [RelayCommand]
    public async Task NavigateToHome()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}
