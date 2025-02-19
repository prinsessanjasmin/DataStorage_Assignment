using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Presentation.MobileApp.ViewModels
{
    public partial class ServiceAddViewModel : ObservableObject
    {

        [RelayCommand]
        public async Task NavigateToHome()
        {
            await Shell.Current.GoToAsync("MainPage");
        }
    }
}
