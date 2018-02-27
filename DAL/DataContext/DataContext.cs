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
        public virtual DbSet<PaperPublishHouses> PaperPublishHouses { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Gener> Geners { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaperPublishHouses>()
                .HasMany(e => e.NewsPapers)
                .WithRequired(e => e.PaperPublishHous)
                .HasForeignKey(e => e.PublishHouse)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Gener>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Gener)
                .HasForeignKey(e => e.Genre)
                .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}
