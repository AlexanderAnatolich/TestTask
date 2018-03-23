using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BookGenerRelations
    {        
        [Key, Column(Order = 1)]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        [Key, Column(Order = 2)]
        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public virtual Gener Gener { get; set; }
    }
}
