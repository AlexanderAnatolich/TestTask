using DAL.Models;
using DAL.DataContext;
using DAL.GenericRepository;
using System.Data.Entity;

namespace DAL.Repositories
{
    public class GenerRepository: EFGenericRepository<Gener>
    {
        public GenerRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}