using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public partial class GenerViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Genre { get; set; }
    }    
}
