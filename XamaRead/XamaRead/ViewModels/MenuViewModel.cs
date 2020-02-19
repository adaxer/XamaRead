using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;
using XamaRead.Views;

namespace XamaRead.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public MenuViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Menu";
        }

        public ICommand ShowSearchCommand => new DelegateCommand(async () => await NavigationService.NavigateAsync($"NavigationPage/{nameof(SearchPage)}"));

        public ICommand ShowWelcomeCommand => new DelegateCommand(async () => await NavigationService.NavigateAsync($"NavigationPage/{nameof(WelcomePage)}"));

    }

}