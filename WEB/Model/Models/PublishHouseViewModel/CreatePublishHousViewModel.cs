using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class CreatePublishHousViewModel
    {
        [Required]
        public string House { get; set; }
    }
}
