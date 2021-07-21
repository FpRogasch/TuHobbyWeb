namespace TuHobbyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_UsersEmail_to_SaleTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sales", "EmailAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "EmailAddress", c => c.String());
        }
    }
}
