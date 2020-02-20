using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace XamaRead.Server
{
    public  class BooksHub : Hub
    {
        public Task ShareBook(string bookInfo)
        {
            return Clients.All.SendAsync("BookShared", bookInfo);
        }
    }
}