using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public partial class NewsPaper
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PrintDate { get; set; }
        public double Price { get; set; }
        public DateTime DateInsert { get; set; }
        [ForeignKey("PublishHouse")]
        public int PublishHouseId { get; set; }
        public virtual PublishHouse PublishHouse { get; set; }
    }
}
