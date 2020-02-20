using System.Collections.Generic;
using System.Threading.Tasks;
using XamaRead.Models;

namespace XamaRead.Interfaces
{
    public interface IDataService
    {
        Task SaveBook(Book book, string notes);
        Task<IEnumerable<BookEntry>> GetEntries();
    }
}