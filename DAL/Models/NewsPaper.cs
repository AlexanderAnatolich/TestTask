using System;

namespace DAL.Models
{
    public partial class NewsPaper
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublishHouse { get; set; }
        public DateTime PrindDate { get; set; }
        public int Price { get; set; }
        public DateTime DateInsert { get; set; }
        public virtual PaperPublishHouses PaperPublishHous { get; set; }
    }
}
