namespace ArtX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        AlbumId = c.Int(nullable: false, identity: true),
                        AlbumTitle = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumId);
            
            CreateTable(
                "dbo.Bookmarks",
                c => new
                    {
                        BookmarkId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Content = c.String(),
                        Rating = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        AlbumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookmarkId)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        BookmarkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Bookmarks", t => t.BookmarkId, cascadeDelete: true)
                .Index(t => t.BookmarkId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "BookmarkId", "dbo.Bookmarks");
            DropForeignKey("dbo.Bookmarks", "AlbumId", "dbo.Albums");
            DropIndex("dbo.Comments", new[] { "BookmarkId" });
            DropIndex("dbo.Bookmarks", new[] { "AlbumId" });
            DropTable("dbo.Comments");
            DropTable("dbo.Bookmarks");
            DropTable("dbo.Albums");
        }
    }
}
