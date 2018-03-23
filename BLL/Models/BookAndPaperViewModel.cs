using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class BookAndPaperViewModel
    {
        public bool Check { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }//ID
        public string Title { get; set; }//Title
        public string Author { get; set; }
        public DateTime YearOfPublish { get; set; }
        public int Price { get; set; }
        public DateTime DateInsert { get; set; }
        public virtual PublishHouseViewModel PublishHouse { get; set; }

    }
}
