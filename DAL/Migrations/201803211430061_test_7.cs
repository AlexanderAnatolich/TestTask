namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test_7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsPapers", "PrintDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.NewsPapers", "PrindDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewsPapers", "PrindDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.NewsPapers", "PrintDate");
        }
    }
}
