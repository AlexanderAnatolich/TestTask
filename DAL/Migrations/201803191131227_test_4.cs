namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test_4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "PublishHouse_Id", "dbo.PublishHouses");
            DropIndex("dbo.Books", new[] { "PublishHouse_Id" });
            AddColumn("dbo.Books", "PublishHouse", c => c.Int(nullable: false));
            DropColumn("dbo.Books", "PublishHouse_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "PublishHouse_Id", c => c.Int());
            DropColumn("dbo.Books", "PublishHouse");
            CreateIndex("dbo.Books", "PublishHouse_Id");
            AddForeignKey("dbo.Books", "PublishHouse_Id", "dbo.PublishHouses", "Id");
        }
    }
}
