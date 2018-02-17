using System;

namespace DAL.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Genre { get; set; }
        public DateTime YearOfPublish { get; set; }
        public string PublishingHouse { get; set; }
        public int Price { get; set; }
        public DateTime DateInsert { get; set; }
        public virtual Gener Gener { get; set; }
    }
}
