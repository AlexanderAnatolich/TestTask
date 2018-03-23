namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test_3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.NewsPapers", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NewsPapers", "Price", c => c.Int(nullable: false));
            AlterColumn("dbo.Books", "Price", c => c.Int(nullable: false));
        }
    }
}
