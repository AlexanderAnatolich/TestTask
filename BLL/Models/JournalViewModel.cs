using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class JournalViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime YearOfPublish { get; set; }
        public double Price { get; set; }
        [ForeignKey("PublishHouse")]
        public DateTime DateInsert { get; set; }
        public int PublishHouseId { get; set; }
        public virtual PublishHouseViewModel PublishHouse { get; set; }
    }
    public class CreateJournalViewModel
    {
        public string Title { get; set; }
        public DateTime YearOfPublish { get; set; }
        public double Price { get; set; }
        [ForeignKey("PublishHouse")]
        public DateTime DateInsert { get; set; }
        public int PublishHouseId { get; set; }
        public virtual PublishHouseViewModel PublishHouse { get; set; }
    }
}
