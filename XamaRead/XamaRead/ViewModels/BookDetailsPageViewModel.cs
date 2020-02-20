using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using XamaRead.Interfaces;
using XamaRead.Models;
using XamaRead.Views;

namespace XamaRead.ViewModels
{
    public class BookDetailsPageViewModel : ViewModelBase
    {
        private readonly IBookService _bookService;
        private readonly IDialogService _dialogService;
        private readonly IShareBooks _shareBooks;
        private Book _currentBook;

        public BookDetailsPageViewModel(INavigationService navigationService, IBookService bookService, IDialogService dialogService, IShareBooks shareBooks) : base(navigationService)
        {
            _bookService = bookService;
            this._dialogService = dialogService;
            this._shareBooks = shareBooks;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.GetValue<string>("BookId") is string id)
            {
                var result = await _bookService.GetBookDetailsAsync(id);
                CurrentBook = result.book;
                Title = $"{CurrentBook.Info.Title} ({result.notes})";
                //var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "lastbook.txt");
                //File.WriteAllText(path, id);
            }
        }

        public Book CurrentBook
        {
            get { return _currentBook; }
            set { SetProperty(ref _currentBook, value); }
        }

        public ICommand SaveCommand => new DelegateCommand(Save);

        public ICommand UploadCommand => new DelegateCommand(Upload);

        private async void Save()
        {
            var parameters = new DialogParameters
            {
                { "Title", "Enter Note" }
            };

            _dialogService.ShowDialog(nameof(EnterTextDialog), parameters, async result =>
            {
                if (result.Parameters.GetValue<bool>("IsOk"))
                {
                    await _bookService.SaveBookAsync(CurrentBook, result.Parameters.GetValue<string>("Value"));
                }
                else
                {
                    Title += " => Not saved";
                }
            });
        }

        private async void Upload()
        {
            await _shareBooks.ShareBook(CurrentBook);
        }
    }
}
