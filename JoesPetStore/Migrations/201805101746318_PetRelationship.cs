namespace JoesPetStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PetRelationship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Receipts", "Pet_Id", c => c.Int());
            CreateIndex("dbo.Receipts", "Pet_Id");
            AddForeignKey("dbo.Receipts", "Pet_Id", "dbo.Pets", "Id");
            DropColumn("dbo.Receipts", "PetId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Receipts", "PetId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Receipts", "Pet_Id", "dbo.Pets");
            DropIndex("dbo.Receipts", new[] { "Pet_Id" });
            DropColumn("dbo.Receipts", "Pet_Id");
        }
    }
}
