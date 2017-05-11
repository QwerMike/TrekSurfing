namespace TrekSurfing.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReceiverId = c.String(nullable: false, maxLength: 128),
                        Message = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ReceiverId, cascadeDelete: true)
                .Index(t => t.ReceiverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "ReceiverId", "dbo.AspNetUsers");
            DropIndex("dbo.Notifications", new[] { "ReceiverId" });
            DropTable("dbo.Notifications");
        }
    }
}
