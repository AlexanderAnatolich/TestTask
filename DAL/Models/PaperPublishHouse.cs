using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public partial class PaperPublishHouses
    {
        public PaperPublishHouses()
        {
            NewsPapers = new HashSet<NewsPaper>();
        }
        public int Id { get; set; }
        public string PublishHouse { get; set; }
        public virtual ICollection<NewsPaper> NewsPapers { get; set; }
    }
}
