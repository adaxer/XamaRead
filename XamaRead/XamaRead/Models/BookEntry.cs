using SQLite;
using System;

namespace XamaRead.Models
{
    public class BookEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string BookId { get; set; }

        public string Title { get; set; }

        public string Notes { get; set; }

        public DateTime Saved { get; set; }
    }
}
