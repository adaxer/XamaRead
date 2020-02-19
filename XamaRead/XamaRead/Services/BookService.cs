using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using XamaRead.Interfaces;
using XamaRead.Models;

namespace XamaRead.Services
{
    public class BookService : IBookService
    {
        public async Task<BookQuery> BookQueryAsync(string text)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string json = await httpClient.GetStringAsync($"https://www.googleapis.com/books/v1/volumes?q={text}&maxResults=10");
                    return JsonConvert.DeserializeObject<BookQuery>(json);
                }
                // Never !!!
                // catch { }
                catch(HttpRequestException ex)
                {
                    Trace.WriteLine(ex.ToString());
                    return null;
                }
                catch(Exception ex1)
                {
                    throw new Exception("Bookquery failed", ex1);
                }
                // Unten passiert automatisch, wenn Exception durchkommt
                //catch(Exception)
                //{
                //    throw;
                //}
            }
        }

        public async Task<(Book book, string notes)> GetBookDetailsAsync(string id)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string json = await httpClient.GetStringAsync($"https://www.googleapis.com/books/v1/volumes/{id}");
                    return (JsonConvert.DeserializeObject<Book>(json), "From Server");
                }
                catch (HttpRequestException ex)
                {
                    Trace.WriteLine(ex.ToString());
                    return (null, "Not found");
                }
            }
        }

        public Task SaveBookAsync(Book book, string notes)
        {
            throw new NotImplementedException();
        }
    }
}
