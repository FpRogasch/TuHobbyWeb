namespace TuHobbyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestoretheApp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "User_UserId", c => c.Int());
            CreateIndex("dbo.Users", "User_UserId");
            AddForeignKey("dbo.Users", "User_UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "User_UserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "User_UserId" });
            DropColumn("dbo.Users", "User_UserId");
        }
    }
}
