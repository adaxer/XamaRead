using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using XamaRead.Interfaces;
using XamaRead.Models;

namespace XamaRead.ViewModels
{
    public class BookDetailsPageViewModel : ViewModelBase
    {
        private readonly IBookService _bookService;
        private Book _currentBook;

        public BookDetailsPageViewModel(INavigationService navigationService, IBookService bookService) : base(navigationService)
        {
            _bookService = bookService;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.GetValue<string>("BookId") is string id)
            {
                var result = await _bookService.GetBookDetailsAsync(id);
                CurrentBook = result.book;
                Title = $"{CurrentBook.Info.Title} ({result.notes})";
            }
        }

        public Book CurrentBook
        {
            get { return _currentBook; }
            set { SetProperty(ref _currentBook, value); }
        }
    }
}
