using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace SignalRTestClient
{
    class Program
    {
        private static HubConnection _connection;

        static void Main(string[] args)
        {
            Console.ReadLine();
            CallSignalR();
            Console.ReadLine();
        }

        private static async void CallSignalR()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://daxbookserver.azurewebsites.net/messagehub")
                //.WithUrl("https://localhost:5001/bookshub")
                .Build();

            _connection.On<string>("BookShared", Console.WriteLine);

            await _connection.StartAsync();
            await _connection.InvokeAsync("ShareBook", "testmessage");


        }
    }
}
