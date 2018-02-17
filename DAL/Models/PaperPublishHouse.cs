using System.Collections.Generic;

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
