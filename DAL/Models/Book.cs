using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime YearOfPublish { get; set; }
        public double Price { get; set; }
        public DateTime DateInsert { get; set; }
        [ForeignKey("PublishHouse")]
        public int PublishHouseId { get; set; }
        public virtual PublishHouse PublishHouse { get; set; }
    }
}
