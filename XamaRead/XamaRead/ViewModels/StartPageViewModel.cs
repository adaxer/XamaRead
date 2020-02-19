using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XamaRead.ViewModels
{
    public class StartPageViewModel : ViewModelBase
    {
        public StartPageViewModel(MenuViewModel menu, INavigationService navigationService) : base(navigationService)
        {
            Menu = menu;
            Title = "XamaRead";
        }

        public MenuViewModel Menu { get; }
    }
}
