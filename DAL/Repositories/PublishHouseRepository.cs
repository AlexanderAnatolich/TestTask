using DAL.Models;
using DAL.GenericRepository;
using System.Data.Entity;

namespace DAL.Repositories
{
    public class PublishHouseRepository : EFGenericRepository<PublishHouse>
    {
        public PublishHouseRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}