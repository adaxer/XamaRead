using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;

namespace XamaRead.ViewModels
{
    public class EnterTextViewModel : ViewModelBase, IDialogAware, IAutoInitialize
    {
        bool _isOk;

        public EnterTextViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        private string _value;
        public string Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        public event Action<IDialogParameters> RequestClose;

        public ICommand OkCommand => new DelegateCommand(() => Close(true));
        public ICommand CancelCommand => new DelegateCommand(() => Close(false));

        private void Close(bool accept)
        {
            _isOk = accept;
            RequestClose?.Invoke(new DialogParameters { { "Value", Value },{ "IsOk", _isOk } });
        }

        public bool CanCloseDialog() => !_isOk || !string.IsNullOrEmpty(_value);

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
    }
}