namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigartion : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DBFeeds", "name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DBFeeds", "name", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
