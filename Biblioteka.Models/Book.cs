using System;

namespace Biblioteka.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishingDate { get; set; }
    }
}
