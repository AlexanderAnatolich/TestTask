using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public partial class GenerViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Genre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookViewModel> Books { get; set; }
    }
    public partial class EditGenerViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Genre { get; set; }
    }
    public partial class CreateGenerViewModel
    {
        [Required]
        public string Genre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookViewModel> Books { get; set; }
    }
    public partial class DeleteGenerViewModel
    {
        [Key]
        public int Id { get; set; }
    }
}
