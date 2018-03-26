using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public class PublishHouseViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string House { get; set; }
    }    
}