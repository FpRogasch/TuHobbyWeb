namespace TuHobbyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTable_Users_and_Roles2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PasswordSalt", c => c.Binary(nullable: false));
            DropColumn("dbo.Users", "PaswordSalt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "PaswordSalt", c => c.Binary(nullable: false));
            DropColumn("dbo.Users", "PasswordSalt");
        }
    }
}
