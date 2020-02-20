using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using XamaRead.Interfaces;
using XamaRead.Models;

namespace XamaRead.Services
{
    public class BookService : IBookService
    {
        IRestService _rest;
        IDataService _dataService;

        public BookService(IRestService rest, IDataService data)
        {
            _rest = rest;
            _dataService = data;
        }

        public async Task<BookQuery> BookQueryAsync(string text)
        {
            var result = await _rest.GetDataAsync<BookQuery>
                (string.Format("https://www.googleapis.com/books/v1/volumes?q={0}&maxResults=40", text));

            string json = JsonConvert.SerializeObject(result.Books);
            return result;

        }

        public async Task<(Book book, string notes)> GetBookDetailsAsync(string id)
        {
            var entry = (await _dataService.GetEntries()).FirstOrDefault(e => e.BookId == id);
            string notes = entry == null
                ? string.Empty
                : entry.Notes;

            Book book = await _rest.GetDataAsync<Book>(string.Format("https://www.googleapis.com/books/v1/volumes/{0}", id));

            return (book, notes);
        }

        public async Task SaveBookAsync(Book book, string notes)
        {
            await _dataService.SaveBook(book, notes);
        }

        public Task UploadBookAsync(Book book, string notes)
        {
            throw new NotImplementedException();
        }
    }
}
