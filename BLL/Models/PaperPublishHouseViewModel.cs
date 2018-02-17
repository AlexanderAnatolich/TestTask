using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class PaperPublishHouseViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PublishHouse { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewsPaperViewModel> NewsPapers { get; set; }
    }
    public class EditPaperPublishHouseViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PublishHouse { get; set; }
    }
    public class CreatePaperPublishHousViewModel
    {
        [Required]
        public string PublishHouse { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewsPaperViewModel> NewsPapers { get; set; }
    }
    public class DeletePaperPublishHouseViewModel
    {
        public int Id { get; set; }
    }
}