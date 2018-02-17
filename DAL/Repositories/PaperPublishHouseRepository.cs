using DAL.Models;
using DAL.GenericRepository;
using System.Data.Entity;

namespace DAL.Repositories
{
    public class PaperPublishHouseRepository : EFGenericRepository<PaperPublishHouses>
    {
        public PaperPublishHouseRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}