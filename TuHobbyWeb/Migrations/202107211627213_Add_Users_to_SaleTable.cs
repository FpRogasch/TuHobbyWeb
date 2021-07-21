namespace TuHobbyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Users_to_SaleTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Sale_SaleId", c => c.Int());
            CreateIndex("dbo.Users", "Sale_SaleId");
            AddForeignKey("dbo.Users", "Sale_SaleId", "dbo.Sales", "SaleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Sale_SaleId", "dbo.Sales");
            DropIndex("dbo.Users", new[] { "Sale_SaleId" });
            DropColumn("dbo.Users", "Sale_SaleId");
        }
    }
}
