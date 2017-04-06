namespace TrekSurfing.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrekEventAndReview : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Double(nullable: false),
                        Message = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Author_Id = c.String(nullable: false, maxLength: 128),
                        Target_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Target_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Target_Id);
            
            CreateTable(
                "dbo.TrekEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Starts = c.DateTime(nullable: false),
                        Ends = c.DateTime(nullable: false),
                        Description = c.String(),
                        Route = c.String(nullable: false),
                        Image = c.Binary(),
                        Created = c.DateTime(nullable: false),
                        Owner_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id, cascadeDelete: true)
                .Index(t => t.Owner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrekEvents", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "Target_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TrekEvents", new[] { "Owner_Id" });
            DropIndex("dbo.Reviews", new[] { "Target_Id" });
            DropIndex("dbo.Reviews", new[] { "Author_Id" });
            DropTable("dbo.TrekEvents");
            DropTable("dbo.Reviews");
        }
    }
}
