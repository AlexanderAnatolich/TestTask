using System.Collections.Generic;

namespace DAL.Models
{
    public class Gener
    {
        public Gener()
        {
            Books = new HashSet<Book>();
        }
        public int Id { get; set; }
        public string Genre { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
