using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using XamaRead.Interfaces;
using XamaRead.Models;
using XamaRead.Views;
using Xamarin.Essentials;

namespace XamaRead.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        #region Private Fields

        private string _searchText;
        private string _resultInfo;
        private Book _book;
        private readonly IBookService _bookService;

        #endregion

        #region Initialization
        public SearchPageViewModel(INavigationService navigationService, IBookService bookService) : base(navigationService)
        {
            Title = "Search";
            this._bookService = bookService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if(Preferences.ContainsKey("LastQuery"))
            {
                SearchText = Preferences.Get("LastQuery", "");
                DoSearch();
            }
        }
        #endregion

        #region Properties

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public string ResultInfo
        {
            get { return _resultInfo; }
            set { SetProperty(ref _resultInfo, value); }
        }

        public Book CurrentBook
        {
            get { return _book; }
            set 
            { 
                SetProperty(ref _book, value); 
                if(_book!=null)
                {
                    NavigationService.NavigateAsync(nameof(BookDetailsPage), ("BookId", _book.Id));
                }
            }
        }

        public IList<Book> Results { get; } = new ObservableCollection<Book>();

        #endregion

        #region Commands

        public ICommand SearchCommand => new DelegateCommand(DoSearch);

        private async void DoSearch()
        {
            Results.Clear();

            if(string.IsNullOrWhiteSpace(SearchText))
            {
                return;
            }

            var bookQuery = await _bookService.BookQueryAsync(SearchText);
            ResultInfo = $"Query Result Count: {bookQuery?.Count}";
            foreach (var book in bookQuery?.Books ?? new List<Book>())
            {
                Results.Add(book);
            }
            Preferences.Set("LastQuery", SearchText);
        }

        #endregion

        #region Methods
        #endregion
    }
}
