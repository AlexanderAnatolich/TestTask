using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class PublishHouseViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string House { get; set; }
    }
    public class CreatePublishHousViewModel
    {
        [Required]
        public string House { get; set; }
    }
    public class DeletePublishHouseViewModel
    {
        public int Id { get; set; }
    }
}