using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public partial class CreateGenerViewModel
    {
        [Required]
        public string Genre { get; set; }
    }
}
