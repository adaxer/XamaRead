using System.Threading.Tasks;
using XamaRead.Models;

namespace XamaRead.Interfaces
{
    public interface IBookService
    {
        Task<BookQuery> BookQueryAsync(string text);

        Task<(Book book, string notes)> GetBookDetailsAsync(string id);

        Task SaveBookAsync(Book book, string notes);

        Task UploadBookAsync(Book book, string notes);
    }
}
