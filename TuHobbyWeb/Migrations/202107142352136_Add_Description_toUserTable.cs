namespace TuHobbyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Description_toUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Descriptiom", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Descriptiom");
        }
    }
}
