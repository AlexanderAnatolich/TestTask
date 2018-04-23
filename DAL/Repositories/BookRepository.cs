using DAL.Models;
using System.Data.Entity;
using DAL.GenericRepository;

namespace DAL.Repositories
{
    public class BookRepository:EFGenericRepository<Book>
    {
        public BookRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}