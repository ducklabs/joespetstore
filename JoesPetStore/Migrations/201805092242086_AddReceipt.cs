namespace JoesPetStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReceipt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Receipts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Receipts");
        }
    }
}
