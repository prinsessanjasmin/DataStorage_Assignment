using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using Data.Migrations;
using System.Collections.ObjectModel;
using System.Data;

namespace Presentation.MobileApp.ViewModels;

public partial class ServiceUpdateViewModel : ObservableObject, IQueryAttributable
{
    private readonly ICompanyServiceService _companyServiceApiService;
    private readonly IUnitService _unitApiService;
    private readonly ICurrencyService _currencyApiService;
    private readonly ServiceListViewModel _serviceListViewModel;

    [ObservableProperty]
    private CompanyServiceEntity _companyService;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private ObservableCollection<CompanyServiceEntity> _companyServices;

    [ObservableProperty]
    private ObservableCollection<UnitEntity> _units;

    [ObservableProperty]
    private ObservableCollection<CurrencyEntity> _currencies;

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private decimal _price;

    [ObservableProperty]
    private int _unitId;

    [ObservableProperty]
    private UnitEntity _selectedUnit;

    [ObservableProperty]
    private int _currencyId;

    [ObservableProperty]
    private CurrencyEntity _selectedCurrency;

    public ServiceUpdateViewModel(ICompanyServiceService companyServiceApiService, IUnitService unitApiService, ICurrencyService currencyApiService, ServiceListViewModel serviceListViewModel)
    {
        _companyServiceApiService = companyServiceApiService;
        _unitApiService = unitApiService;
        _currencyApiService = currencyApiService;
        _serviceListViewModel = serviceListViewModel;

        _units = new ObservableCollection<UnitEntity>();
        _currencies = new ObservableCollection<CurrencyEntity>();
        _companyService = new CompanyServiceEntity();
    }

    [RelayCommand]
    public async Task SaveChanges()
    {
        CompanyService.Title = Title;
        CompanyService.Price = Price;

        CompanyService.UnitId = SelectedUnit.Id;
        CompanyService.Unit = SelectedUnit;

        CompanyService.CurrencyId = SelectedCurrency.Id;
        CompanyService.Currency = SelectedCurrency;

        var finishedCompanyService = await _companyServiceApiService.UpdateCompanyService(CompanyService.Id, CompanyService);
        if (finishedCompanyService != null)
        {
            await Shell.Current.GoToAsync("CompanyServiceListPage");
            ErrorMessage = "Service updated successfully!";
        }
        else
        {
            ErrorMessage = _companyServiceApiService.ErrorMessage;
        }
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        //Method constructed by ChatGPT 4o

        if (query.TryGetValue("companyServiceId", out var idValue) && int.TryParse(idValue.ToString(), out int companyServiceId))
        {
            CompanyService.Id = companyServiceId;
            await LoadCompanyServiceDetails();
        }
    }

    private async Task LoadCompanyServiceDetails()
    {
        var companyService = await _companyServiceApiService.GetCompanyServiceById(CompanyService.Id);
        if (companyService != null)
        {
            Title = companyService.Title;
            Price = companyService.Price;
            
            Units = new ObservableCollection<UnitEntity>(await _unitApiService.GetAllUnits());
            SelectedUnit = Units.FirstOrDefault(x => x.Id == companyService.Unit.Id);

            Currencies = new ObservableCollection<CurrencyEntity>(await _currencyApiService.GetAllCurrencies());
            SelectedCurrency = Currencies.FirstOrDefault(x => x.Id == companyService.Currency.Id);
        }
        else
        {
            ErrorMessage = "Error loading service details.";
        }
    }

    [RelayCommand]
    public async Task DeleteService(int id)
    {
        bool result = await _companyServiceApiService.DeleteCompanyService(CompanyService.Id);
        if (result)
        {
            await _serviceListViewModel.LoadCompanyServices();
            await Shell.Current.GoToAsync("ServiceListPage");
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
