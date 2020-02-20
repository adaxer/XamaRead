using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using XamaRead.Models;

namespace XamaRead.Events
{
    public class BookSharedEvent : PubSubEvent<Book>
    {
    }
}
