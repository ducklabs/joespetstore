namespace JoesPetStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingApproval : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Approvals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerEmail = c.String(),
                        ApprovalState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Approvals");
        }
    }
}
