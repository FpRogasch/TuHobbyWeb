namespace TuHobbyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ProductImage_To_ProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductImage", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ProductImage");
        }
    }
}
