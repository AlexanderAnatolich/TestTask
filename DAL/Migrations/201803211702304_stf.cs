namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "PublishHouse_Id", "dbo.PublishHouses");
            DropIndex("dbo.Books", new[] { "PublishHouse_Id" });
            RenameColumn(table: "dbo.Books", name: "PublishHouse_Id", newName: "PublishHouseId");
            AlterColumn("dbo.Books", "PublishHouseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "PublishHouseId");
            AddForeignKey("dbo.Books", "PublishHouseId", "dbo.PublishHouses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "PublishHouseId", "dbo.PublishHouses");
            DropIndex("dbo.Books", new[] { "PublishHouseId" });
            AlterColumn("dbo.Books", "PublishHouseId", c => c.Int());
            RenameColumn(table: "dbo.Books", name: "PublishHouseId", newName: "PublishHouse_Id");
            CreateIndex("dbo.Books", "PublishHouse_Id");
            AddForeignKey("dbo.Books", "PublishHouse_Id", "dbo.PublishHouses", "Id");
        }
    }
}
