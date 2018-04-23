using DAL.Models;
using DAL.GenericRepository;
using System.Data.Entity;

namespace DAL.Repositories
{
    public class NewsPapersRepository : EFGenericRepository<NewsPaper>
    {
        public NewsPapersRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}