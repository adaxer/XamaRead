using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Prism.Events;
using System;
using System.Threading.Tasks;
using XamaRead.Events;
using XamaRead.Interfaces;
using XamaRead.Models;

namespace XamaRead.Services
{
    public class ShareBooks : IShareBooks
    {
        private HubConnection _connection;
        private IEventAggregator _events;

        public ShareBooks(IEventAggregator events)
        {
            _events = events;
            Initialize();
        }

        private async void Initialize()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://daxbookserver.azurewebsites.net/messagehub")
                //.WithUrl("http://localhost:5000/bookshub")
                .Build();

            _connection.On<string>("BookShared", RaiseBookShared);

            try
            {
                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void RaiseBookShared(string bookInfo)
        {
            _events.GetEvent<BookSharedEvent>().Publish(JsonConvert.DeserializeObject<Book>(bookInfo));
        }

        public async Task ShareBook(Book book)
        {
            try
            {
                await _connection.InvokeAsync("ShareBook", JsonConvert.SerializeObject(book));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
