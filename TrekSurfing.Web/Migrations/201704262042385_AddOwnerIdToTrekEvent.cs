namespace TrekSurfing.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOwnerIdToTrekEvent : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TrekEvents", name: "Owner_Id", newName: "OwnerId");
            RenameIndex(table: "dbo.TrekEvents", name: "IX_Owner_Id", newName: "IX_OwnerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TrekEvents", name: "IX_OwnerId", newName: "IX_Owner_Id");
            RenameColumn(table: "dbo.TrekEvents", name: "OwnerId", newName: "Owner_Id");
        }
    }
}
