using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamaRead.Models;

namespace XamaRead.Interfaces
{
    public interface IShareBooks
    {
        Task ShareBook(Book book);
    }
}
