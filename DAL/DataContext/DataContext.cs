using DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using DAL.Models.AuthorizationModel;

namespace DAL.DataContext
{
    public class DataContex : IdentityDbContext<ApplicationUser>
    {      
        public DataContex() : base("MyDBConnection", throwIfV1Schema:false)
        {

        }
        public DataContex(string connectionString) : base(connectionString)
        {

        }
        public static DataContex Create()
        {
            return new DataContex();
        }
        public virtual DbSet<NewsPaper> NewsPapers { get; set; }
        public virtual DbSet<PublishHouse> PublishHouses { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Gener> Geners { get; set; }

        public virtual DbSet<Journal> Journals { get; set; }
        public virtual DbSet<BookGenerRelations> BookGenerRelations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
