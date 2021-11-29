using WPFDragDrop.Entities;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDragDrop.DataAccess
{
    public class IADbContext : DbContext
    {
        public IADbContext()
            : base("IAConnString")
        {
            Database.CommandTimeout = base.Database.Connection.ConnectionTimeout;
            this.Configuration.LazyLoadingEnabled = false;

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<IADbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }

        public DbSet<Entity> Entity { get; set; }
        public DbSet<EntityAttribute> EntityAttribute { get; set; }
    }
}
