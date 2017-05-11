namespace TrekSurfing.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConfirmedToTrekEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrekEvents", "Confirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrekEvents", "Confirmed");
        }
    }
}
