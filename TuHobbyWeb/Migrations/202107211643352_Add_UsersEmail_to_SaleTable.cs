namespace TuHobbyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UsersEmail_to_SaleTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Sale_SaleId", "dbo.Sales");
            DropIndex("dbo.Users", new[] { "Sale_SaleId" });
            AddColumn("dbo.Sales", "EmailAddress", c => c.String());
            DropColumn("dbo.Users", "Sale_SaleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Sale_SaleId", c => c.Int());
            DropColumn("dbo.Sales", "EmailAddress");
            CreateIndex("dbo.Users", "Sale_SaleId");
            AddForeignKey("dbo.Users", "Sale_SaleId", "dbo.Sales", "SaleId");
        }
    }
}
