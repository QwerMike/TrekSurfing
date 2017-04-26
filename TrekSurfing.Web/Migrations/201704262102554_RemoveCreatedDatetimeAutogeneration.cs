namespace TrekSurfing.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCreatedDatetimeAutogeneration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrekEvents", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrekEvents", "Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Reviews", "Created", c => c.DateTime(nullable: false));
        }
    }
}
