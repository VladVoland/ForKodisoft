namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DBContentCollections",
                c => new
                    {
                        ContentCollectionId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ContentCollectionId);
            
            CreateTable(
                "dbo.DBFeeds",
                c => new
                    {
                        FeedId = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 30),
                        URL = c.String(nullable: false),
                        ContentCollection_ContentCollectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FeedId)
                .ForeignKey("dbo.DBContentCollections", t => t.ContentCollection_ContentCollectionId, cascadeDelete: true)
                .Index(t => t.ContentCollection_ContentCollectionId);
            
            CreateTable(
                "dbo.DBUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Surname = c.String(nullable: false, maxLength: 30),
                        Patronymic = c.String(nullable: false, maxLength: 30),
                        Login = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DBFeeds", "ContentCollection_ContentCollectionId", "dbo.DBContentCollections");
            DropIndex("dbo.DBFeeds", new[] { "ContentCollection_ContentCollectionId" });
            DropTable("dbo.DBUsers");
            DropTable("dbo.DBFeeds");
            DropTable("dbo.DBContentCollections");
        }
    }
}
