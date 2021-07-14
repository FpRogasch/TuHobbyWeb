namespace TuHobbyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Sales_Models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SaleLines",
                c => new
                    {
                        SaleLineId = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        SaleId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.SaleLineId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Sales", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        SubTotal = c.Int(nullable: false),
                        Tax = c.Int(nullable: false),
                        ConfirmedAt = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.Users", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Users");
            DropForeignKey("dbo.SaleLines", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.SaleLines", "ProductId", "dbo.Products");
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.SaleLines", new[] { "SaleId" });
            DropIndex("dbo.SaleLines", new[] { "ProductId" });
            DropTable("dbo.Sales");
            DropTable("dbo.SaleLines");
        }
    }
}
