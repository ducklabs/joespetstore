namespace JoesPetStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PetIdAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Receipts", "Pet_Id", "dbo.Pets");
            DropIndex("dbo.Receipts", new[] { "Pet_Id" });
            RenameColumn(table: "dbo.Receipts", name: "Pet_Id", newName: "PetId");
            AlterColumn("dbo.Receipts", "PetId", c => c.Int(nullable: false));
            CreateIndex("dbo.Receipts", "PetId");
            AddForeignKey("dbo.Receipts", "PetId", "dbo.Pets", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Receipts", "PetId", "dbo.Pets");
            DropIndex("dbo.Receipts", new[] { "PetId" });
            AlterColumn("dbo.Receipts", "PetId", c => c.Int());
            RenameColumn(table: "dbo.Receipts", name: "PetId", newName: "Pet_Id");
            CreateIndex("dbo.Receipts", "Pet_Id");
            AddForeignKey("dbo.Receipts", "Pet_Id", "dbo.Pets", "Id");
        }
    }
}
