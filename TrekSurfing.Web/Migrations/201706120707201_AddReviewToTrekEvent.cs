namespace TrekSurfing.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReviewToTrekEvent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "Target_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Reviews", new[] { "Author_Id" });
            DropIndex("dbo.Reviews", new[] { "Target_Id" });
            RenameColumn(table: "dbo.Reviews", name: "Author_Id", newName: "AuthorId");
            RenameColumn(table: "dbo.Reviews", name: "Target_Id", newName: "TargetId");
            AlterColumn("dbo.Reviews", "AuthorId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Reviews", "TargetId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "AuthorId");
            CreateIndex("dbo.Reviews", "TargetId");
            AddForeignKey("dbo.Reviews", "AuthorId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Reviews", "TargetId", "dbo.TrekEvents", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "TargetId", "dbo.TrekEvents");
            DropForeignKey("dbo.Reviews", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Reviews", new[] { "TargetId" });
            DropIndex("dbo.Reviews", new[] { "AuthorId" });
            AlterColumn("dbo.Reviews", "TargetId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Reviews", "AuthorId", c => c.String(nullable: false, maxLength: 128));
            RenameColumn(table: "dbo.Reviews", name: "TargetId", newName: "Target_Id");
            RenameColumn(table: "dbo.Reviews", name: "AuthorId", newName: "Author_Id");
            CreateIndex("dbo.Reviews", "Target_Id");
            CreateIndex("dbo.Reviews", "Author_Id");
            AddForeignKey("dbo.Reviews", "Target_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Reviews", "Author_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
