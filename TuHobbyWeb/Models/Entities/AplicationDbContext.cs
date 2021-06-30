using System;
using System.Data.Entity;
using System.Linq;

namespace TuHobbyWeb.Models.Entities
{
    public class AplicationDbContext : DbContext
    {
        // Your context has been configured to use a 'AplicationDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TuHobbyWeb.Models.Entities.AplicationDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AplicationDbContext' 
        // connection string in the application configuration file.
        public AplicationDbContext()
            : base("name=AplicationDbContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}