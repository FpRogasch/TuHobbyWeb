namespace TuHobbyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UserToken_To_UserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserToken", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserToken");
        }
    }
}
