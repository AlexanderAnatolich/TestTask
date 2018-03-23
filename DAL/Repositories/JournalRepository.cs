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
    public class JournalRepository : EFGenericRepository<Journal>
    {
        public JournalRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
