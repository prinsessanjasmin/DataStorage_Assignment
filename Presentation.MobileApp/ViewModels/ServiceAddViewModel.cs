using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using Data.Migrations;
using System.Collections.ObjectModel;
using System.Data;

namespace Presentation.MobileApp.ViewModels
{
    public partial class ServiceAddViewModel : ObservableObject
    {
        private readonly ICompanyServiceService _companyServiceApiService;
        private readonly IUnitService _unitApiService;
        private readonly ICurrencyService _currencyApiService;

        [ObservableProperty]
        private CompanyServiceModel _companyService;

        [ObservableProperty]
        private string? _errorMessage;

        [ObservableProperty]
        private ObservableCollection<UnitEntity> _units;

        [ObservableProperty]
        private ObservableCollection<CurrencyEntity> _currencies;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _price;

        [ObservableProperty]
        private UnitEntity _selectedUnit;

        [ObservableProperty]
        private CurrencyEntity _selectedCurrency;

        public ServiceAddViewModel(ICompanyServiceService companyApiService, IUnitService unitApiService, ICurrencyService currencyApiService)
        {
            _companyServiceApiService = companyApiService;
            _unitApiService = unitApiService;
            _currencyApiService = currencyApiService;
            _companyService = new CompanyServiceModel();
            _units = new ObservableCollection<UnitEntity>();
            _currencies = new ObservableCollection<CurrencyEntity>();

            LoadCurrencies().ConfigureAwait(false);
            LoadUnits().ConfigureAwait(false);
        }

        [RelayCommand]
        public async Task SaveChanges()
        {
            if (string.IsNullOrWhiteSpace(Title) ||
            string.IsNullOrWhiteSpace(Price) ||
            SelectedCurrency == null ||
            SelectedUnit == null)
            {
                ErrorMessage = "All fields must be filled out.";
                return;
            }

            CompanyService.ServiceTitle = Title;
            CompanyService.Price = decimal.Parse(Price);
            CompanyService.UnitId = SelectedUnit.Id;
            CompanyService.CurrencyId = SelectedCurrency.Id;

            var createdCompanyService = await _companyServiceApiService.CreateCompanyService(CompanyService);
            if (createdCompanyService != null)
            {
                Title = string.Empty;
                Price = null!;
                SelectedUnit = null!;
                SelectedCurrency = null!;


                ErrorMessage = "Service created successfully!";
            }
            else
            {
                ErrorMessage = _companyServiceApiService.ErrorMessage;
            }
        }

        [RelayCommand]
        public async Task LoadUnits()
        {
            try
            {
                var unitList = await _unitApiService.GetAllUnits();
                if (unitList != null)
                {
                    Units.Clear();
                    foreach (var unit in unitList)
                    {
                        Units.Add(unit);
                    }
                    ErrorMessage = _unitApiService.ErrorMessage;
                }
                else
                {
                    ErrorMessage = _unitApiService.ErrorMessage;
                }
            }
            catch
            {
                ErrorMessage = _unitApiService.ErrorMessage;
            }
        }

        [RelayCommand]
        public async Task LoadCurrencies()
        {
            try
            {
                var currencyList = await _currencyApiService.GetAllCurrencies();
                if (currencyList != null)
                {
                    Currencies.Clear();
                    foreach (var currency in currencyList)
                    {
                        Currencies.Add(currency);
                    }
                    ErrorMessage = _currencyApiService.ErrorMessage;
                }
                else
                {
                    ErrorMessage = _currencyApiService.ErrorMessage;
                }
            }
            catch
            {
                ErrorMessage = _currencyApiService.ErrorMessage;
            }
        }

        [RelayCommand]
        public async Task NavigateToServiceList()
        {
            await Shell.Current.GoToAsync("ServiceListPage");
        }

        [RelayCommand]
        public async Task NavigateToHome()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
