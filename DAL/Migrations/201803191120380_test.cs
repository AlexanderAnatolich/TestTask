namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BookGenerRelations", "BookId");
            AddForeignKey("dbo.BookGenerRelations", "BookId", "dbo.Books", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookGenerRelations", "BookId", "dbo.Books");
            DropIndex("dbo.BookGenerRelations", new[] { "BookId" });
        }
    }
}
