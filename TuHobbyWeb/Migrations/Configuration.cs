namespace TuHobbyWeb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TuHobbyWeb.Helpers;
    using TuHobbyWeb.Models.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<TuHobbyWeb.Models.Entities.AplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Roles.Any())
            {
                context.Roles.Add(new Role { RoleName = StringHelper.ROLE_ADMINISTRATOR});
                context.Roles.Add(new Role { RoleName = StringHelper.ROLE_USER });
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var role = context.Roles.FirstOrDefault(x => x.RoleName == StringHelper.ROLE_ADMINISTRATOR);
                if (role != null)
                {
                    PasswordHelper.CreatePassword("123456", out byte[] passwordHash, out byte[] passwordSalt);
                    context.Users.Add(new User
                    {
                        CreatedAt = DateTime.Now,
                        EmailAddress = "pablo.matias@gmail.com",
                        FirstName = "Pablo",
                        LastName = "Poblete",
                        RoleId = role.RoleId,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        VerifiedAt = DateTime.Now
                    });
                }
            }
        }
    }
}
