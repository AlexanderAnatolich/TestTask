using DAL.GenericRepository;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BookGenerRelationsRepository : EFGenericRepository<BookGenerRelations>
    {
        DbContext _dbContext;
        private DbSet<BookGenerRelations> _dbSet;
        public BookGenerRelationsRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<BookGenerRelations>();
        }      
    }
}
