using System;

namespace Biblioteka.Models.Queries
{
    public class BookQuery
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime? PublishingDate { get; set; }
    }
}
