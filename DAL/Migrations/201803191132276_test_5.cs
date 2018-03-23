namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test_5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "PublishHouse_Id", c => c.Int());
            CreateIndex("dbo.Books", "PublishHouse_Id");
            AddForeignKey("dbo.Books", "PublishHouse_Id", "dbo.PublishHouses", "Id");
            DropColumn("dbo.Books", "PublishHouse");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "PublishHouse", c => c.Int(nullable: false));
            DropForeignKey("dbo.Books", "PublishHouse_Id", "dbo.PublishHouses");
            DropIndex("dbo.Books", new[] { "PublishHouse_Id" });
            DropColumn("dbo.Books", "PublishHouse_Id");
        }
    }
}
