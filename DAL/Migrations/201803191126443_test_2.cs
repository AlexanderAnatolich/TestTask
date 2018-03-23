namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test_2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BookGenerRelations", "GenreId");
            AddForeignKey("dbo.BookGenerRelations", "GenreId", "dbo.Geners", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookGenerRelations", "GenreId", "dbo.Geners");
            DropIndex("dbo.BookGenerRelations", new[] { "GenreId" });
        }
    }
}
