namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class KodiDB : DbContext
    {
        public KodiDB()
            : base("name=KodiDB")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        }

        public DbSet<DBFeed> Feeds { get; set; }
        public DbSet<DBContentCollection> ContentCollections { get; set; }
        public DbSet<DBUser> Users { get; set; }
    }
}