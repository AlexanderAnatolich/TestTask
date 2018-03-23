namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class np1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NewsPapers", "PublishHouse_Id", "dbo.PublishHouses");
            DropIndex("dbo.NewsPapers", new[] { "PublishHouse_Id" });
            RenameColumn(table: "dbo.NewsPapers", name: "PublishHouse_Id", newName: "PublishHouseId");
            AlterColumn("dbo.NewsPapers", "PublishHouseId", c => c.Int(nullable: false));
            CreateIndex("dbo.NewsPapers", "PublishHouseId");
            AddForeignKey("dbo.NewsPapers", "PublishHouseId", "dbo.PublishHouses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsPapers", "PublishHouseId", "dbo.PublishHouses");
            DropIndex("dbo.NewsPapers", new[] { "PublishHouseId" });
            AlterColumn("dbo.NewsPapers", "PublishHouseId", c => c.Int());
            RenameColumn(table: "dbo.NewsPapers", name: "PublishHouseId", newName: "PublishHouse_Id");
            CreateIndex("dbo.NewsPapers", "PublishHouse_Id");
            AddForeignKey("dbo.NewsPapers", "PublishHouse_Id", "dbo.PublishHouses", "Id");
        }
    }
}
