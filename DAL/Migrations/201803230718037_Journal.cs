namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Journal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Journals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        YearOfPublish = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        DateInsert = c.DateTime(nullable: false),
                        PublishHouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PublishHouses", t => t.PublishHouseId, cascadeDelete: true)
                .Index(t => t.PublishHouseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Journals", "PublishHouseId", "dbo.PublishHouses");
            DropIndex("dbo.Journals", new[] { "PublishHouseId" });
            DropTable("dbo.Journals");
        }
    }
}
