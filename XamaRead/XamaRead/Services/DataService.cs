using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XamaRead.Interfaces;
using XamaRead.Models;

namespace XamaRead.Services
{
    // Gemäß https://docs.microsoft.com/de-de/xamarin/get-started/tutorials/local-database/?tabs=vswin&tutorial-step=2
    public class DataService : IDataService
    {
        string _DBPath;
        private SQLiteAsyncConnection _connection;

        //private ISQLiteConnectionFactory m_ConnectionFactory;

        public DataService()
        {
            _DBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "books.db");
        }

        public async Task<IEnumerable<BookEntry>> GetEntries()
        {
            var conn = await GetConnection();
            return await conn.Table<BookEntry>().ToListAsync();
        }

        public async Task SaveBook(Book book, string notes)
        {
            var connection = await GetConnection();
            var entry = await _connection.Table<BookEntry>().FirstOrDefaultAsync(be => be.BookId == book.Id);
            if (entry == null)
            {
                await connection.InsertAsync(new BookEntry { BookId = book.Id, Title = book.Info.Title, Saved = DateTime.UtcNow, Notes = notes });
            }
            else
            {
                entry.Notes = notes;
                await connection.UpdateAsync(entry);
            }
        }

        private async Task<SQLiteAsyncConnection> GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SQLiteAsyncConnection(_DBPath);
                await _connection.CreateTableAsync<BookEntry>();
            }
            return _connection;
        }
    }
}
