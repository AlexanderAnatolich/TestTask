using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class SummaryViewModel
    {
        public bool Check { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime YearOfPublish { get; set; }
        public int Price { get; set; }
        public DateTime DateInsert { get; set; }
        public PublishHouseViewModel PublishHouse { get; set; }

    }
}
