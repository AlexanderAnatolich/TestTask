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
        public string Author { get; set; }//PublishHouse
        public DateTime YearOfPublish { get; set; }//PrintDate
        public int Price { get; set; }//Prise
        public DateTime DateInsert { get; set; }//DateInsert

    }
}
