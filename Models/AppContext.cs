using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ArtX.Models
{
    public class AppContext : DbContext
    {

        public AppContext() : base("DBConnectionString")
        {
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppContext,
            //ArtX.Migrations.Configuration>("DBConnectionString"));
        }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Comment> Comments { get; set; }
    
    }
}